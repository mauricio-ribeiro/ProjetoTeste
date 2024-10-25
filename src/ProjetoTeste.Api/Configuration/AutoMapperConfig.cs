using System.Reflection;

namespace ProjetoTeste.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            // Registra automaticamente todos os perfis que herdam de Profile no assembly atual
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
