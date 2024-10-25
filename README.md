# ProjetoTeste
 Desenvolvimento de Web API em C# conceito em DDD e Testes Unitários com xUnit e Mock

 Arquivo appsettings.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local);Database=FornecedoresDB;User Id=seu_usuario;Password=sua_senha;MultipleActiveResultSets=true"
  },
  "AllowedHosts": "*"
}

Incluir o código seed abaixo caso precise...

 // Seed com 5 registros de fornecedor
 migrationBuilder.InsertData(
     table: "Fornecdor",
     columns: new[] { "NomeFantasia", "RazaoSocial", "Documento", "Ativo", "CriadoEm" },
     values: new object[,]
     {
         { "Kaique e Hadassa Corretores Associados ME", "Kaique e Hadassa Corretores Associados ME", "65934619000119", true, DateTime.Now },
         { "Ian e Benjamin Casa Noturna Ltda", "Ian e Benjamin Casa Noturna Ltda", "61109778000128", true, DateTime.Now },
         { "Maria e Victor Fotografias ME", "Maria e Victor Fotografias ME", "51724403000114", false, DateTime.Now },
         { "Stefany e Raquel Esportes Ltda", "Stefany e Raquel Esportes Ltda", "03635196000189", true, DateTime.Now },
         { "Stefany e Sérgio Construções Ltda", "Stefany e Sérgio Construções Ltda", "59133403000151", false, DateTime.Now }
     });
