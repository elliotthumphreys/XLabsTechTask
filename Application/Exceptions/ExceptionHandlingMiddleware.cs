using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Application.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                await SendReturnableExceptionResponse(httpContext, HttpStatusCode.NotFound, ex, "Not found");
            }
            catch (ValidationException ex1)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                await SendReturnableExceptionResponse(httpContext, HttpStatusCode.BadRequest, ex1, "One or more validation errors have occurred");
            }
            catch (Exception)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                await SendExceptionResponse(httpContext);
            }
        }
        private static async Task SendReturnableExceptionResponse(HttpContext httpContext, HttpStatusCode statusCode, Exception ex, string title)
        {
            httpContext.Response.StatusCode = (int)statusCode;

            var errors = new Dictionary<string, object>();

            if(ex.Data.Keys.Count > 0)
            {
                foreach(var key in ex.Data.Keys)
                {
                    var value = ex.Data[key];

                    if (value is null)
                        continue;

                    errors.Add(key.ToString() ?? Guid.NewGuid().ToString(), value);
                }
            }
            
            await SendResponse(new ErrorResponse
            {
                Type = ex.GetType().Name,
                Title = title,
                Detail = ex.Message,
                Status = statusCode,
                Errors = errors
            }, httpContext);
        }

        private static async Task SendExceptionResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 500;
            
            await SendResponse(new ErrorResponse
            {
                Type = "ServerError",
                Title = "Internal Server Error",
                Detail = "Internal Server Error",
                Status = (HttpStatusCode)httpContext.Response.StatusCode,
                Errors = new Dictionary<string, object>()
            }, httpContext);
        }

        private static async Task SendResponse(ErrorResponse response, HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(SerialiseToJsonString(response));
        }

        private static string SerialiseToJsonString(object data)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }
    }

    public record ErrorResponse
    {
        public string Type { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Detail { get; set; } = default!;
        public HttpStatusCode Status { get; set; }
        public object? Errors { get; set; }
    }
}
