namespace ProjetoTeste.Api.Configuration
{
    public static class MiddlewareConfig
    {
        public static IApplicationBuilder UseMiddlewareConfig(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            return app;
        }

        public static IApplicationBuilder UseEndpointsConfig(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            return app;
        }
    }
}
