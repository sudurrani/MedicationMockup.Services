using Microsoft.Extensions.DependencyInjection.Extensions;
using MedicationMockup.API.Services;
using MedicationMockup.Application;
using MedicationMockup.Application.Shared.Common.Interfaces;
using MedicationMockup.Application.Shared.Interfaces;
using MedicationMockup.Core.Shared.Interfaces;
using MedicationMockup.Core.Shared.Services;
using MedicationMockup.EntityFrameworkCore.Repositories;

namespace MedicationMockup.API.Dependencies
{
    public static class DependencyInjection
    {
       
        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddTransient(typeof(IEFCoreRepository<>), typeof(EFCoreRepository<>));
            collection.AddTransient<IUserAppService, UserAppService>();
            collection.AddTransient<IConnectionString, ConnectionString>();
            
            collection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            collection.AddTransient<IClaimsService, ClaimsService>();

       

            collection.AddTransient<IFileUploadService, FileUploadService>();
            collection.AddTransient<IMedicationAppService, MedicationAppService>();

           
        }
    }
}
