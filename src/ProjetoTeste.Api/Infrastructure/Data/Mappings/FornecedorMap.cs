using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Api.Domain.Entities;

namespace ProjetoTeste.Api.Infrastructure.Data.Mappings
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecdor");

            builder.HasKey(u => u.Id);

            // Propriedades
            builder.Property(u => u.Id).IsRequired();
            builder.Property(u => u.NomeFantasia).HasMaxLength(100).IsRequired();
            builder.Property(u => u.RazaoSocial).HasMaxLength(150).IsRequired();
            builder.Property(u => u.Documento).IsRequired();

            builder.Property(c => c.Endereco).HasMaxLength(100).IsRequired(false);
            builder.Property(c => c.Numero).HasMaxLength(10).IsRequired(false);
            builder.Property(c => c.Bairro).HasMaxLength(50).IsRequired(false);
            builder.Property(c => c.Cep).HasMaxLength(8).IsRequired(false);
            builder.Property(c => c.Complemento);
            builder.Property(c => c.Cidade).HasMaxLength(60).IsRequired(false);

            builder.Property(c => c.Uf)
                .HasColumnType("char(2)")
                .IsRequired(false);

            builder.Property(c => c.Responsavel).HasMaxLength(50).IsRequired(false);
            builder.Property(c => c.TelFone1).HasMaxLength(20).IsRequired(false);
            builder.Property(c => c.TelFone2).HasMaxLength(20).IsRequired(false);
            builder.Property(c => c.Email).HasMaxLength(150).IsRequired(false);
            builder.Property(u => u.Ativo).IsRequired();

            // Propriedades de auditoria da BaseEntity
            builder.Property(u => u.CriadoEm).IsRequired();
            builder.Property(u => u.AlteradoEm).IsRequired(false);
        }
    }
}
