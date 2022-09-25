using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Common.Interfaces;
using MedicationMockup.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.EntityFrameworkCore.Repositories
{
    public class EFCoreRepository<TEntity> : IEFCoreRepository<TEntity> where TEntity : class
    {
        private readonly MedicationMockupDbContext _dbContext;
        internal DbSet<TEntity> dbSet;
        ResponseOutputDto _responseOutputDto;
        private readonly IConfiguration _configuration;
        public EFCoreRepository(MedicationMockupDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
            _configuration = configuration;

            _responseOutputDto = new ResponseOutputDto();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public async Task<TEntity> GetById(long id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
            //.AsNoTracking()
            //.FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<long> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<long> Update(TEntity entity)
        {
            //_dbContext.Entry(entity).Property("").IsModified = false;
            //_dbContext.Entry(entity).Property("CreatedBy").IsModified = false;
            //_dbContext.Entry(entity).Property("CreatedDate").IsModified = false;
            //_dbContext.Entry(entity).Property("DeletedBy").IsModified = false;
            //_dbContext.Entry(entity).Property("DeletedDate").IsModified = false;
            _dbContext.Set<TEntity>().Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<long> Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            //_dbContext.Set<TEntity>().Update(entity).Property("CreatedBy").IsModified = false;
            //_dbContext.Set<TEntity>().Update(entity).Property("CreatedDate").IsModified = false;
            //_dbContext.Set<TEntity>().Update(entity).Property("UpdatedBy").IsModified = false;
            //_dbContext.Set<TEntity>().Update(entity).Property("UpdatedDate").IsModified = false;
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<long> AddRange(List<TEntity> entity)
        {
            _dbContext.AddRange(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
        public async Task<long> UpdateRange(List<TEntity> entity)
        {
            _dbContext.Set<TEntity>().UpdateRange(entity);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<long> DeleteRange(List<TEntity> entity)
        {
            _dbContext.Set<TEntity>().UpdateRange(entity);
            return await _dbContext.SaveChangesAsync();
        }
        #region Procedures with Dapper
        public async Task<IEnumerable<T>> GetAll<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class
        {
            using (var conn = new SqlConnection(_configuration["AppSettings:ConnectionString"]))
            {
                return await conn.QueryAsync<T>(queryTextOrProcedureName, dynamicParameters, commandType: isCommandTypeProcedure == true ? CommandType.StoredProcedure : CommandType.Text);
            }
        }
        public async Task<T> GetSingle<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class
        {
            using (var conn = new SqlConnection(_configuration["AppSettings:ConnectionString"]))
            {
                return await conn.QueryFirstOrDefaultAsync<T>(queryTextOrProcedureName, dynamicParameters, commandType: isCommandTypeProcedure == true ? CommandType.StoredProcedure : CommandType.Text);
            }
        }
        public async Task<int> Execute<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class
        {
            using (var conn = new SqlConnection(_configuration["AppSettings:ConnectionString"]))
            {
                return await conn.ExecuteAsync(queryTextOrProcedureName, dynamicParameters, commandType: isCommandTypeProcedure == true ? CommandType.StoredProcedure : CommandType.Text);
            }

        }
        #endregion
    }
}
