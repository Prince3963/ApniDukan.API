using ApniDukan.Application.RepoInterface;
using ApniDukan.Domain.Models;
using ApniDukan.Infrastrcture.Data;
using Microsoft.EntityFrameworkCore;

namespace ApniDukan.Infrastrcture.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : GenericEntity
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<T> dbSet;
        public GenericRepo(AppDbContext dbContext, DbSet<T> dbSet)
        {
            this.dbContext = dbContext;
            this.dbSet = dbSet;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
