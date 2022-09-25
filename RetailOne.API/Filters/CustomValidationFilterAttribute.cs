using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MedicationMockup.Application.Shared.Common.Dtos;

namespace MedicationMockup.API.Filters
{
    public class CustomValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var _responseOutputDto = new ResponseOutputDto();
                Dictionary<string, List<string>> errorDictionary = new Dictionary<string, List<string>>();
                Dictionary<string, object> responseDictionary = new Dictionary<string, object>();
                var errors = context.ModelState.Values.Select(rows => rows.Errors);
                var keys = context.ModelState.Keys.ToList();
                List<string> stringsList = new List<string>();
                int rowIndex = 0, rowCount = 1;
                string errorMessages = string.Empty;

                foreach (var error in errors)
                {
                    stringsList = new List<string>();
                    var key = keys[rowIndex];
                    foreach (var er in error)
                    {
                        errorMessages = errorMessages == string.Empty ? (rowCount.ToString() + ". " + er.ErrorMessage.ToString()) : errorMessages + "\n" + (rowCount.ToString() + ". " + er.ErrorMessage.ToString());
                        string errorMsg = er.ErrorMessage.ToString();
                        stringsList.Add(errorMsg);
                        rowCount += 1;
                    }
                    rowIndex++;
                    errorDictionary.Add(key, stringsList);
                }
                responseDictionary.Add("errors", errorDictionary);


                //_responseOutputDto.BadRequest<object>(responseDictionary);
                _responseOutputDto.Invalid(errorMessages);
                //context.Result = new BadRequestObjectResult(_responseOutputDto);
                context.Result = new OkObjectResult(_responseOutputDto);
            }

            base.OnActionExecuting(context);
        }
    }
}
