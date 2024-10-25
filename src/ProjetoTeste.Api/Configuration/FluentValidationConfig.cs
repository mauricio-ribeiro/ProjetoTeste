using FluentValidation;
using System.Reflection;

namespace ProjetoTeste.Api.Configuration
{
    public static class FluentValidationConfig
    {
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            // Registra automaticamente todos os validadores no assembly atual
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
