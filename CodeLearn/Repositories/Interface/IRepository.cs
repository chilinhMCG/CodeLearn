using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(Guid id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity id);
    }
}
