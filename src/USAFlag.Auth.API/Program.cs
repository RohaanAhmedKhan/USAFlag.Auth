
using System.Threading.RateLimiting;
using USAFlag.Auth.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add multiple JSON configuration files
AddMultipleJsonFiles(builder.Configuration);


builder.Services.AddEasyCaching(options => { options.UseInMemory("default"); });

 #region Health Checks
builder.Services.AddHealthChecks();
#endregion

#region Authorization
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});
#endregion

#region "Setup DI"

builder.Services.AddScoped<DbContext>();

var ETIAssembly = Assembly.Load("USAFlag.Auth.Core");
// Refister 
builder.Services.RegisterAssemblyPublicNonGenericClasses(ETIAssembly)
    .Where(x => x.Name.EndsWith("Service"))
    .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

// Register Repositories
builder.Services.RegisterAssemblyPublicNonGenericClasses(ETIAssembly)
    .Where(x => x.Name.EndsWith("Repository"))
    .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
#endregion

#region MadiatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ETIAssembly));
#endregion

#region Rate Limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("fixed", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});
#endregion

#region ApiVersioning
builder.Services.AddVersioning();
#endregion

#region ResponseCaching
builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 64 * 1024 * 1024;
    options.SizeLimit = 100 * 1024 * 1024;
    options.UseCaseSensitivePaths = true;
});
#endregion

#region Routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);
#endregion

#region Cors
builder.Services.AddCustomCors("AllowAllOrigins");
#endregion

#region "Controllers"
builder.Services.AddControllers();
#endregion

#region Documentation
ConfigSwagger(builder.Services);
#endregion

var app = builder.Build();

#region Documentation
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth WebApi V1");
    c.DisplayRequestDuration();
});
#endregion

#region "Error Handling"
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}
#endregion

#region HealthCheck
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});
#endregion

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();

void AddMultipleJsonFiles(IConfigurationBuilder configurationBuilder)
{
    string path = Path.Combine(Directory.GetCurrentDirectory(), "Configurations");
    if (Directory.Exists(path))
    {
        string[] files = Directory.GetFiles(path, "*.json");
        foreach (var file in files)
        {
            configurationBuilder.AddJsonFile(file, optional: true, reloadOnChange: true);
        }
    }
}

void ConfigSwagger(IServiceCollection services)
{
    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Auth Web API",
            Version = "v1"
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });
}

public partial class Program { }
