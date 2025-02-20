using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace USAFlag.AuthAPI.Extension
{
    /// <summary>
    /// The Global Exception Handler
    /// </summary>
    public static class ExceptionExtension
    {
        public static void AddProductionExceptionHandling(this IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/plain";
                   
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Global exception logger");
                        logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                    }

                    if (env.IsDevelopment())
                    {
                        await context.Response.WriteAsync(errorFeature.Error.StackTrace);
                    }
                    else
                    {
                        await context.Response.WriteAsync("Sorry! There was a problem in the API call. " +
                        "Please contact the Administrator.");
                    }
                });
            });
        }
    }
}
