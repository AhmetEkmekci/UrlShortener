using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Infrastructure
{
    public static class ExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UrlShortenerBusinessExceptionHandler(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    context.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is UrlShortener.Business.BusinessException.URLShortenerBusinessException)
                    {
                        var exception = exceptionHandlerPathFeature?.Error as UrlShortener.Business.BusinessException.URLShortenerBusinessException;
                        await context.Response.WriteAsJsonAsync(new { Status = "Error", ErrorMessage = exception.Message });
                    }
                    else
                    {
                        await context.Response.WriteAsJsonAsync(new { Status = "Error", ErrorMessage = "An exception was thrown." });
                    }

                });
            });

            return app;
        }
    }
}
