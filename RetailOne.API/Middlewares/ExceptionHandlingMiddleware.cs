using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MedicationMockup.Application.Shared.Common.Dtos;

namespace MedicationMockup.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var _responseOutputDto = new ResponseOutputDto()
            {
                IsSuccess = false
            };


            //if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            //{
            _responseOutputDto.Message = "Internal Server errors. Check Logs!";
            var errorDetail = exception.InnerException == null ? exception.Message : exception.InnerException.Message.ToString();
            _responseOutputDto.InternalServerError<object>(new object(), errorDetail);
            //var interimObject = JsonConvert.DeserializeObject<ExpandoObject>(myJsonInput);
            //var myJsonOutput = JsonConvert.SerializeObject(interimObject, jsonSerializerSettings);

            //Console.Write(myJsonOutput);
            //}
            //_logger.LogError(exception.Message);
            //var result = JsonSerializer.Serialize(_responseOutputDto);
            //await context.Response.WriteAsync(result);
            await context.Response.WriteAsync(
                   JsonConvert.SerializeObject(_responseOutputDto, jsonSerializerSettings));//new ResponseOutputDto ResponseModel("some-message")));

        }
    }
}
