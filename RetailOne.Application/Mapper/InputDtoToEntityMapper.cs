
using AutoMapper;
using MedicationMockup.Application.Shared.Dtos.Medication;
using MedicationMockup.Application.Shared.Dtos.User;
using MedicationMockup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPlace.Application.Mapper
{
    public class InputDtoToEntityMapper : Profile
    {
        public InputDtoToEntityMapper()
        {
            CreateMap<UserInputDto, User>();
            CreateMap<MedicationInputDto, Medication>();
           
          
        }
    }
}
