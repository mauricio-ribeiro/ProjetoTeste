using ProjetoTeste.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configura as dependências
builder.Services.ResolveDependencies(builder.Configuration, builder.Environment);

// Configura o AutoMapper
builder.Services.AddAutoMapperConfig();

// Configura o FluentValidation
builder.Services.AddFluentValidationConfig();

// Configura os serviços
builder.Services.AddCorsConfig();
builder.Services.AddControllersConfig();
builder.Services.AddSwaggerConfig();

var app = builder.Build();

// Configura a aplicação (middlewares e endpoints)
app.UseMiddlewareConfig();
app.UseSwaggerConfig();
app.UseCorsConfig();
app.UseEndpointsConfig();

app.Run();
