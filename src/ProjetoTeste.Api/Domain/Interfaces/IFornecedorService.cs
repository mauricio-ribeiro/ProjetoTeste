using System.Linq.Expressions;
using ProjetoTeste.Api.Domain.Entities;

namespace ProjetoTeste.Api.Domain.Interfaces
{
    public interface IFornecedorService
    {
        Task<Fornecedor> GetByIdAsync(int id);
        Task<IEnumerable<Fornecedor>> GetAllAsync();
        Task AddAsync(Fornecedor fornecedor);
        Task UpdateAsync(Fornecedor fornecedor);
        Task DeleteAsync(int id);
        Task<IEnumerable<Fornecedor>> SearchAsync(Expression<Func<Fornecedor, bool>> filter = null,
           Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = null,
           string includeProperties = "");
        Task<Fornecedor> GetByDocumentoAsync(string documento);
    }
}
