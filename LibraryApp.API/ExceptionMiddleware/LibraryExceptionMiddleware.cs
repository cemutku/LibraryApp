using LibraryApp.API.Models;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Threading.Tasks;

namespace LibraryApp.API.ExceptionMiddleware
{
    public class LibraryExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LibraryExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Something went wrong");
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return httpContext.Response.WriteAsync(new ErrorDetail
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "An error occured while processing the request."
            }.ToString());
        }
    }
}
