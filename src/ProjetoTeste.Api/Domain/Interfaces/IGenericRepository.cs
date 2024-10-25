using System.Linq.Expressions;
using ProjetoTeste.Api.Domain.Entities;

namespace ProjetoTeste.Api.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    }
}
