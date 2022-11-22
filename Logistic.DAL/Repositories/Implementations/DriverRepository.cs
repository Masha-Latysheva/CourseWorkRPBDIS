using System;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Driver, bool>> GetByIdExpression(int entityId)
        {
            return driver => driver.Id == entityId;
        }

        protected override IQueryable<Driver> IncludeAllChildren(IQueryable<Driver> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.Transportations)
                : query;
        }
    }
}