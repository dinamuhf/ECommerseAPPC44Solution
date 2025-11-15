using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Text.Json;

namespace ECommerseAPPC04.CustomsMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private ILogger<CustomExceptionHandlerMiddleWare> _Logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _ = Next;
            _Logger = logger;
        }

        public ILogger<CustomExceptionHandlerMiddleWare> Logger { get; }

        public async Task InvokeAsync(HttpContext httpContext) 
        
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandelNotFountEndPoint(httpContext);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Some Thing Went Wrong");
                await HandelExceptionAsync(httpContext, ex);

            }

        }

        private static async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.ContentType = "application/json";
            var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            //var ResponseToReturn= JsonSerializer.Serialize(Response);
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static async Task HandelNotFountEndPoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The EndPoint{httpContext.Request.Path} Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
