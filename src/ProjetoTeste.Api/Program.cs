using ProjetoTeste.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configura as depend�ncias
builder.Services.ResolveDependencies(builder.Configuration, builder.Environment);

// Configura o AutoMapper
builder.Services.AddAutoMapperConfig();

// Configura o FluentValidation
builder.Services.AddFluentValidationConfig();

// Configura os servi�os
builder.Services.AddCorsConfig();
builder.Services.AddControllersConfig();
builder.Services.AddSwaggerConfig();

var app = builder.Build();

// Configura a aplica��o (middlewares e endpoints)
app.UseMiddlewareConfig();
app.UseSwaggerConfig();
app.UseCorsConfig();
app.UseEndpointsConfig();

app.Run();
