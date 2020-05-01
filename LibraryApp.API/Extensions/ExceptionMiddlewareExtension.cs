using LibraryApp.API.ExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace LibraryApp.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LibraryExceptionMiddleware>();
        }
    }
}
