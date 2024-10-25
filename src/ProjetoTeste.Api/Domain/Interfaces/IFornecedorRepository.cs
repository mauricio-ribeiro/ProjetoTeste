using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Domain.Interfaces;

namespace ProjetoTeste.Api.Domain.Interfaces
{
    public interface IFornecedorRepository : IGenericRepository<Fornecedor>
    {
        Task<Fornecedor> GetByDocumentoAsync(string documento);
    }
}
