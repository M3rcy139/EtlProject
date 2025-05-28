namespace EtlProject.ServiceCollection;

public static class MiddlewareConfuguration
{
    public static IApplicationBuilder ConfigureMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
            });
        }
        else
        {
            app.UseExceptionHandler("/error");
            app.UseHsts();
        }
            
        app.UseHttpsRedirection();
        app.UseRouting();

        app.ConfigureCustomMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}