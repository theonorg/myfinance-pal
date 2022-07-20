
namespace Tiberna.MyFinancePal.GlobalFramework.Extensions.Service;
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddCustomLogger(this WebApplicationBuilder builder, bool reset = true)
    {
        if (reset)
        {
            builder.Logging.ClearProviders();
        }

        builder.Logging.AddSimpleConsole(opts =>
        {
            opts.IncludeScopes = false;
            opts.TimestampFormat = Constants.LoggerTimestampFormat;
            opts.ColorBehavior = LoggerColorBehavior.Disabled;
        });

        return builder;
    }

    public static WebApplicationBuilder AddCustomCORS(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
                {
                    options.AddPolicy(Constants.CorsPolicyName,
                        builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });

        return builder;
    }


    public static WebApplicationBuilder AddCustomSwagger(this WebApplicationBuilder builder, string appName, string version)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"MyFinancePal - {appName}",
                Version = version
            });
        });

        return builder;
    }

}
