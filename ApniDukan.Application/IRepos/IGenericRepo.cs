using ApniDukan.Domain.Models;

namespace ApniDukan.Application.RepoInterface
{
    public interface IGenericRepo<T> where T : GenericEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        void Update(T entity);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
