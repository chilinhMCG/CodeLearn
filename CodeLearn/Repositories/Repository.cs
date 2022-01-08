using CodeLearn.Data;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{

    /// <summary>
    /// Abstract Repository with common functionalities
    /// Repository that inherit this class, it's entity must not be a composite primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity, it's primary key must not be a composite primary key.</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbContextFactory<ApplicationDBContext> _dbContextFactory;

        protected IDbContextFactory<ApplicationDBContext> DbContextFactory => _dbContextFactory;

        public Repository(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            using var context = DbContextFactory.CreateDbContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            using var context = DbContextFactory.CreateDbContext();
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            using var context = DbContextFactory.CreateDbContext();
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            using var context = DbContextFactory.CreateDbContext();
            context.Set<TEntity>().Attach(entity);
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
