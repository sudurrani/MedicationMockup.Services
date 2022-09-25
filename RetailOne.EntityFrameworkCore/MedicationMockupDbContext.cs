using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using MedicationMockup.Core.Common;
using MedicationMockup.Core.Entities;
using MedicationMockup.Core.Shared.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MedicationMockup.EntityFrameworkCore.EntityConfiguration;

namespace MedicationMockup.EntityFrameworkCore
{
    public class MedicationMockupDbContext : DbContext
    {
        private readonly IDateTime _dateTime;
        protected readonly IConfiguration _configuration;
        private readonly IConnectionString _connectionString;
        private readonly IClaimsService _claimsService;
       
        public MedicationMockupDbContext(DbContextOptions<MedicationMockupDbContext> options, IConnectionString connectionString
            , IClaimsService claimsService)
            : base(options)
        {
            _connectionString = connectionString;
            _claimsService = claimsService;
        }

        public MedicationMockupDbContext(
            DbContextOptions<MedicationMockupDbContext> options,
            IDateTime dateTime,
            IConfiguration configuration,
            IConnectionString connectionString,
            IClaimsService claimsService
            )
            : base(options)
        {
            _dateTime = dateTime;
            _configuration = configuration;
            _connectionString = connectionString;
            _claimsService = claimsService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Medication> Medications{ get; set; }
       
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicationEntityTypeConfiguration());
            
            
            

            //Below code implement generic query filter to get only those Entities having IsDeleted = false
            Expression <Func<AuditedEntity, bool>> filterExpr = bm => !bm.IsDeleted;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(AuditedEntity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _connectionString.GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer(@"Server=.;Database=ARPlace;USER ID = sa; Password=P@kistan;");

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var loggedInUserId = _claimsService.GetClaim("Id") == "" ? 0 : Convert.ToInt64(_claimsService.GetClaim("Id"));
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<AuditedEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedById = loggedInUserId;
                    entry.Entity.CreatedDate = _dateTime == null ? DateTime.Now : _dateTime.Now;
                    entry.Entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.IsDeleted == true)
                    {
                        entry.Property("CreatedById").IsModified = false;
                        entry.Property("UpdatedById").IsModified = false;
                        entry.Property("CreatedDate").IsModified = false;
                        entry.Property("UpdatedDate").IsModified = false;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.UpdatedById = loggedInUserId;
                        entry.Entity.DeletedDate = _dateTime == null ? DateTime.Now : _dateTime.Now;
                    }
                    else
                    {
                        entry.Property("CreatedById").IsModified = false;
                        entry.Property("CreatedDate").IsModified = false;
                        entry.Entity.UpdatedById = loggedInUserId;
                        entry.Entity.UpdatedDate = _dateTime == null ? DateTime.Now : _dateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedById = loggedInUserId;
                    entry.Entity.DeletedDate = _dateTime == null ? DateTime.Now : _dateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
   
}
