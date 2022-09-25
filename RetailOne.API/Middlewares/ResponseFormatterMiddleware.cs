using Newtonsoft.Json;
using MedicationMockup.Application.Shared.Common.Dtos;

namespace MedicationMockup.API.Middlewares
{
    public class ResponseFormatterMiddleware
    {
        ResponseOutputDto _responseOutputDto = new ResponseOutputDto();
        private readonly RequestDelegate _next;

        public ResponseFormatterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);

            if (context.Response.StatusCode == 401)//StatusCodes.Status401Unauthorized)
            {
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(_responseOutputDto.Status401Unauthorized()));//new ResponseOutputDto ResponseModel("some-message")));

            }
        }

    }
}
