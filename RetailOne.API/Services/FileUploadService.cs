using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Core.Shared.Interfaces;

namespace MedicationMockup.API.Services
{
    public class FileUploadService : IFileUploadService
    {
        ResponseOutputDto _responseOutputDto;
        public FileUploadService()
        {
            _responseOutputDto = new ResponseOutputDto();
        }
       public async Task<ResponseOutputDto> Image(dynamic objectsInputDto, string absolutePath, double allowedSizeInMbs, string pathToUploadImage = @"Product\Images")
        {
            try
            {
                if (objectsInputDto != null)
                {
                    if (objectsInputDto.Image != null)
                    {
                        double fileSizeibBytes = objectsInputDto.Image.Length;
                        double fileSizeibKbs = fileSizeibBytes / 1024;
                        double fileSizeibMbs = fileSizeibBytes / (1024 * 1024);
                        var mb = Math.Round(fileSizeibMbs, 1);
                        objectsInputDto.ImageExtension = Path.GetExtension(objectsInputDto.Image.FileName);
                        objectsInputDto.ImageOrignalName = Path.GetFileName(objectsInputDto.Image.FileName);
                        objectsInputDto.ImageNewName = Guid.NewGuid().ToString() + "" + objectsInputDto.ImageExtension;
                        //Set the image location under WWWRoot folder.                
                        objectsInputDto.ImageRelatvePath = Path.Combine(pathToUploadImage, objectsInputDto.ImageNewName);
                        objectsInputDto.ImageAbsolutePath = Path.Combine(absolutePath, pathToUploadImage, objectsInputDto.ImageNewName); // Path.Combine(_environment.WebRootPath, @"Images\3d2d", objectsInputDto.ImageNewName);
                        if (!string.IsNullOrEmpty(objectsInputDto.ImageExtension) && (!objectsInputDto.ImageExtension.ToString().Trim().ToLower().Equals(".jpg") && !objectsInputDto.ImageExtension.Trim().ToLower().Equals(".jpeg") && !objectsInputDto.ImageExtension.Trim().ToLower().Equals(".png")))
                        {
                            _responseOutputDto.Warning($"Image(s) with extension .jpg, jpeg & .png are only allowed");
                        }
                        else if (mb > allowedSizeInMbs)
                        {
                            _responseOutputDto.Warning($"Image(s) are allowed only with maximum size of {allowedSizeInMbs}");
                        }
                        else
                        {

                            //Saving the file in that folder 
                            using (FileStream stream = new FileStream(objectsInputDto.ImageAbsolutePath, FileMode.Create))
                            {

                                await objectsInputDto.Image.CopyToAsync(stream);
                                stream.Close();
                                _responseOutputDto.Success<object>(objectsInputDto);
                            }

                        }
                    }
                    else
                    {
                        _responseOutputDto.Success<object>(objectsInputDto); ;
                    }
                }
                else
                {
                    _responseOutputDto.Success<object>(objectsInputDto); ;
                }

            }
            catch (Exception ex)
            {
                _responseOutputDto.Error(ex.Message);

            }
            return _responseOutputDto;
        }
    }
}
