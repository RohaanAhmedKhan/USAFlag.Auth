using Microsoft.Extensions.DependencyInjection;

namespace USAFlag.AuthAPI.Extension
{
    public static class CorsExtension
    {
        /// <summary>
        /// Configure the CORS Settings for better security
        ///  - Most importantly the allow origin settings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="policyName"></param>
        public static void AddCustomCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
