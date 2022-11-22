using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Repositories.Abstractions
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        public Task<TEntity> GetEntityById(int entityId, bool asNoTracking = true, bool includeAllChildren = true);

        public IQueryable<TEntity> QueryEntities(bool asNoTracking = true, bool includeAllChildren = true);

        public Task Delete(int id);

        public Task Add(TEntity entity);

        public Task Update(TEntity entity);
    }
}
