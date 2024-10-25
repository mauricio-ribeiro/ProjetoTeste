using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Api.Application.Interfaces;
using ProjetoTeste.Api.Application.Services;
using ProjetoTeste.Api.Domain.Interfaces;
using ProjetoTeste.Api.Domain.Services;
using ProjetoTeste.Api.Infrastructure.Data.Context;
using ProjetoTeste.Api.Infrastructure.Repositories;

namespace ProjetoTeste.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, 
            IConfiguration configuration, 
            IHostEnvironment environment)
        {
            // Registra as instâncias específicas de IConfiguration e IHostEnvironment
            services.AddSingleton(configuration);
            services.AddSingleton(environment);

            // Configuração de repositórios
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            // Configuração de serviços            
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IFornecedorAppService, FornecedorAppService>();            

            // Adiciona o DbContext e configura a connection string do SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);

            return services;
        }
    }
}
