using System.Linq.Expressions;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Domain.Interfaces;

namespace ProjetoTeste.Api.Domain.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IConfiguration _configuration;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IConfiguration configuration)
        {
            _fornecedorRepository = fornecedorRepository;
            _configuration = configuration;
        }

        public async Task<Fornecedor> GetByIdAsync(int id)
        {
            return await _fornecedorRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            return await _fornecedorRepository.GetAllAsync();
        }

        public async Task AddAsync(Fornecedor fornecedor)
        {
            await _fornecedorRepository.AddAsync(fornecedor);
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            await _fornecedorRepository.UpdateAsync(fornecedor);
        }

        public async Task DeleteAsync(int id)
        {
            await _fornecedorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Fornecedor>> SearchAsync(Expression<Func<Fornecedor, bool>> filter = null,
             Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = null,
             string includeProperties = "")
        {
            var empresas = await _fornecedorRepository.GetAllAsync();

            // Aplica o filtro, se houver
            if (filter != null)
                empresas = empresas.AsQueryable().Where(filter);

            // Aplica a ordenação, se houver
            if (orderBy != null)
                empresas = orderBy(empresas.AsQueryable());

            // O parâmetro includeProperties não está sendo utilizado na implementação do repositório, mas poderia ser utilizado para incluir propriedades relacionadas se o repositório suportar isso.

            return empresas.ToList();
        }

        public async Task<Fornecedor> GetByDocumentoAsync(string documento)
        {
            return await _fornecedorRepository.GetByDocumentoAsync(documento);
        }
    }
}
