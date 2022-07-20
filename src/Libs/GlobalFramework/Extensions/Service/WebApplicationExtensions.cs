namespace Tiberna.MyFinancePal.GlobalFramework.Extensions.Service;

public static class WebApplicationExtensions
{
    public static WebApplication UseCustomSwagger(this WebApplication app, string appName)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} V1");
            });
            app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
        }

        return app;
    }

    public static WebApplication SetExceptionPage(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");

            app.MapGet("/error", (HttpContext context) =>
            {
                var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var badRequestEx = error as BadHttpRequestException;
                var statusCode = badRequestEx?.StatusCode ?? StatusCodes.Status500InternalServerError;
                const string contentType = "application/problem+json";
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = contentType;
                return Results.Problem(badRequestEx?.Message);
            })
            .ExcludeFromDescription();

            app.MapFallback(() => Results.Redirect("/error"));
        }

        return app;
    }

    public static WebApplication UseCustomCORS(this WebApplication app)
    {
        app.UseCors(Constants.CorsPolicyName);
        //app.UseHttpsRedirection();

        return app;
    }

}