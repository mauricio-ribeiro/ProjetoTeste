using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Domain.Interfaces;
using ProjetoTeste.Api.Infrastructure.Data.Context;

namespace ProjetoTeste.Api.Infrastructure.Repositories
{
    public class FornecedorRepository : GenericRepository<Fornecedor>, IFornecedorRepository
    {          
        public FornecedorRepository(AppDbContext context) : base(context)
        {
       
        }

        public async Task<Fornecedor> GetByDocumentoAsync(string documento)
        {
            return await _context.Fornecedores
               .FirstOrDefaultAsync(e => e.Documento == documento);
        }
    }
}
