using System.Linq.Expressions;
using System.Threading.Tasks;
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
        public GenericRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existEntity = await dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (existEntity == null || existEntity.IsDeleted)
            {
                return false;
            }

            existEntity.IsDeleted = true;
            existEntity.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return true;            
        }


        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var existEntity = await dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (existEntity.IsDeleted || existEntity == null)
            {
                return null;
            }

            return existEntity;
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var itExist = await dbSet.AsNoTracking().AnyAsync(predicate, cancellationToken);
            return itExist;
        }
    }
}
