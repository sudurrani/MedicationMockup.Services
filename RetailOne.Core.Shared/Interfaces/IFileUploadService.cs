using MedicationMockup.Application.Shared.Common.Dtos;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Core.Shared.Interfaces
{
    public interface IFileUploadService
    {
        //ResponseOutputDto CreateObjectValidation(ObjectsInputDto objectsInputDto, double allowedSizeForObjectInMbs, double allowedSizeForImageInMbs);
        //ResponseOutputDto UpdateObjectValidation(ObjectsInputDto objectsInputDto, double allowedSizeForObjectInMbs, double allowedSizeForImageInMbs);
        //public Task<ResponseOutputDto> Objects(ObjectsInputDto objectsInputDto, string absolutePath);
        Task<ResponseOutputDto> Image(dynamic objectsInputDto, string absolutePath, double allowedSizeInMbs, string pathToUploadImage = @"Product\Images");
    }
}
