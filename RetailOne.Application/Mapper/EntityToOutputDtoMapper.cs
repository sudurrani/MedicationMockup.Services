
using AutoMapper;
using MedicationMockup.Application.Shared.Dtos.Medication;
using MedicationMockup.Application.Shared.Dtos.User;
using MedicationMockup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Mapper
{
    public class EntityToOutputDtoMapper : Profile
    {
        public EntityToOutputDtoMapper()
        {
            CreateMap<User, UserOutputDto>();
            CreateMap<User, UserLoginOutputDto>();            
            CreateMap<Medication, MedicationOutputDto>();            
            
           
        }
    }
}
