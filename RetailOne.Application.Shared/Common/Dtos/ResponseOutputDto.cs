using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Common.Dtos
{
    public class ResponseOutputDto
    {
        public bool IsSuccess { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic resultJSON { get; set; }

        public void Success<DtoType>(DtoType resultJSON, string message = null) where DtoType : class
        {
            this.IsSuccess = true;
            this.Status = "Success";
            this.Message = message == null ? "Success"  : message;
            this.resultJSON = resultJSON;
        }
        public void Error(string? message= null)
        {
            this.IsSuccess = false;
            this.Status = "Error";
            this.Message = message == null ? "An error occured while processing your request" : message;
            this.resultJSON = new object();
        }
        public void Invalid( string? message = null)
        {
            this.IsSuccess = false;
            this.Status = "Invalid";
            this.Message = message == null ? "An error occured while processing your request" : message;
            this.resultJSON = new object();
        }
        public void Warning(string? message = null)
        {
            this.IsSuccess = false;
            this.Status = "Warning";
            this.Message = message == null ? "An error occured while processing your request" : message;
            this.resultJSON  = new object();
        }
        public void BadRequest<DtoType>(DtoType resultJSON) where DtoType : class
        {
            this.IsSuccess = false;
            this.Status = "400";
            this.Message = "Bad Request";
            this.resultJSON = resultJSON;
        }
        public ResponseOutputDto Status401Unauthorized()
        {
            ResponseOutputDto responseOutputDto = new ResponseOutputDto();
            responseOutputDto.IsSuccess = false;
            responseOutputDto.Status = "Unauthorized";
            responseOutputDto.Message = "You're Unauthorized either do not have valid token";
            responseOutputDto.resultJSON = new object();
            return responseOutputDto;
        }
        public void InternalServerError<DtoType>(DtoType resultJSON,string message) where DtoType : class
        {
            this.IsSuccess = false;
            this.Status = "500";
            this.Message = message;
            this.resultJSON = resultJSON;
        }
    }
}
