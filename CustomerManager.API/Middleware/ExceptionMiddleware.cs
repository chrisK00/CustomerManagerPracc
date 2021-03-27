using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerManager.API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerManager.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    AppException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var response = _env.IsDevelopment() ?
                      new ApiException(context.Response.StatusCode, ex?.Message, ex?.StackTrace) :
                      new ApiException(context.Response.StatusCode, ex?.Message);

                var result = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                await context.Response.WriteAsync(result);
            }
        }

    }
}
