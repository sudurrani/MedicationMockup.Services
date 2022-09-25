using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Interfaces
{
    public interface IUserAppService
    {
        #region POST Requests
        Task<ResponseOutputDto> Login(UserInputDto userInputDto);
        Task<ResponseOutputDto> Create(UserInputDto userInputDto);
        Task<ResponseOutputDto> Update(UserInputDto userInputDto);
        Task<ResponseOutputDto> Delete(UserInputDto userInputDto);

        #endregion

        #region GET Requests

        Task<ResponseOutputDto> GetAll();
        Task<ResponseOutputDto> GetById(long id);
        #endregion
    }
}
