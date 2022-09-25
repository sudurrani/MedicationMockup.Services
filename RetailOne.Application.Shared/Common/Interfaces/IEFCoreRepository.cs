using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Core.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Common.Interfaces
{
    public interface IEFCoreRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(long id);

        Task<long> Create(TEntity entity);

        Task<long> Update(TEntity entity);

        Task<long> Delete(TEntity entity);
        Task<long> AddRange(List<TEntity> entity);
        Task<long> UpdateRange(List<TEntity> entity);
        Task<long> DeleteRange(List<TEntity> entity);

        //Task<ResponseOutputDto> GetMultipleAsync<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class;
        Task<IEnumerable<T>> GetAll<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class;
        Task<T> GetSingle<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class;
        Task<int> Execute<T>(string queryTextOrProcedureName, SqlDynamicParameters dynamicParameters, bool isCommandTypeProcedure = true) where T : class;
    }
}
