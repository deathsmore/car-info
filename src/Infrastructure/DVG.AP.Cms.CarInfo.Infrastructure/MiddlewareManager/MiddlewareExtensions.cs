using Microsoft.AspNetCore.Builder;

namespace DVG.AP.Cms.CarInfo.Infrastructure.MiddlewareManager
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}