using System.Linq.Expressions;
using ProjetoTeste.Api.Application.DTOs;

namespace ProjetoTeste.Api.Application.Interfaces
{
    public interface IAppServiceBase<TEntity, TResponse, TCreateRequest, TUpdateRequest>
        where TEntity : class
        where TResponse : class
    {
        Task<Response<TResponse>> AdicionarAsync(TCreateRequest request);
        Task<Response<TResponse>> AtualizarAsync(int id, TUpdateRequest request);
        Task<Response<TResponse>> ExcluirAsync(int id);
        Task<Response<TResponse>> ObterPorIdAsync(int id);
        Task<Response<IEnumerable<TResponse>>> ObterTodosAsync();
        Task<Response<IEnumerable<TResponse>>> BuscarAsync(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
    }
}
