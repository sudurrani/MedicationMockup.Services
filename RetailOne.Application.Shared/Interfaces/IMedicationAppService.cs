using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Dtos.Medication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Interfaces
{
    public interface IMedicationAppService
    {
        #region Get Requests
        Task<ResponseOutputDto> GetAll();
        Task<ResponseOutputDto> GetMy(long id);
        Task<ResponseOutputDto> GetById(long id);
        #endregion

        #region Post Requests
        Task<ResponseOutputDto> Create(MedicationInputDto medicationInputDto);
        Task<ResponseOutputDto> Update(MedicationInputDto medicationInputDto);
        Task<ResponseOutputDto> Delete(MedicationInputDto medicationInputDto);
        #endregion
    }
}
