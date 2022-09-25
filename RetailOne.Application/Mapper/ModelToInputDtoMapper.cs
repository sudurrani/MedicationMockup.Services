
using AutoMapper;
using MedicationMockup.Application.Shared.Dtos.Medication;
using MedicationMockup.Application.Shared.Dtos.User;
using MedicationMockup.Application.Shared.Models.Medication;
using MedicationMockup.Application.Shared.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Mapper
{
    public class ModelToInputDtoMapper : Profile
    {
        public ModelToInputDtoMapper()
        {
            #region User
            CreateMap<UserRegisterModel, UserInputDto>();
            CreateMap<UserLoginModel, UserInputDto>();
            CreateMap<UserUpdateModel, UserInputDto>();
            CreateMap<UserDeleteModel, UserInputDto>();
            #endregion
            #region Medication
            CreateMap<MedicationCreateModel, MedicationInputDto>();
            CreateMap<MedicationUpdateModel, MedicationInputDto>();
            CreateMap<MedicationDeleteModel, MedicationInputDto>();
            
            #endregion

        }
    }
}
