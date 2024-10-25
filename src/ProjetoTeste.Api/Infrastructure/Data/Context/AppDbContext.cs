using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Extensions;
using ProjetoTeste.Api.Infrastructure.Data.Mappings;

namespace ProjetoTeste.Api.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Fornecedor> Fornecedores { get; set; }
        
        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.ApplyConfiguration(new FornecedorMap());
            
            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity);

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CriadoEm = DateTime.Now;
                    entity.AlteradoEm = null; // Apenas para garantir que AlteradoEm fique null ao adicionar
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(entity.CriadoEm)).IsModified = false; // Evita a alteração de CriadoEm
                    entity.AlteradoEm = DateTime.Now;
                }
            }
        }
    }
}
