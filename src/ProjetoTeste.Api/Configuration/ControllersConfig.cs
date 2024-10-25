namespace ProjetoTeste.Api.Configuration
{
    public static class ControllersConfig
    {
        public static IServiceCollection AddControllersConfig(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddNewtonsoftJson();

            return services;
        }
    }
}
