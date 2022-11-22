using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Repositories.Abstractions
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private bool _disposed;


        protected LogisticContext Context { get; }


        protected BaseRepository(LogisticContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> QueryEntities(bool asNoTracking = true, bool includeAllChildren = true)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return IncludeAllChildren(query, includeAllChildren);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetEntityById(id, includeAllChildren: false);
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            Context.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetEntityById(int entityId, bool asNoTracking = true, bool includeAllChildren = true)
        {
            var expression = GetByIdExpression(entityId);
            var entity = await QueryEntities(asNoTracking, includeAllChildren)
                .FirstOrDefaultAsync(expression);

            return entity;
        }

        protected abstract Expression<Func<TEntity, bool>> GetByIdExpression(int entityId);


        protected abstract IQueryable<TEntity> IncludeAllChildren(IQueryable<TEntity> query, bool includeAllChildren);

        #region IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Context.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}
