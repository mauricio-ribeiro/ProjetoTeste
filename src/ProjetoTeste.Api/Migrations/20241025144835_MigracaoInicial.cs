using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTeste.Api.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecdor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Uf = table.Column<string>(type: "char(2)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Responsavel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TelFone1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TelFone2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecdor", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fornecdor");
        }
    }
}
