using System;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class PointRepository : BaseRepository<Point>, IPointRepository
    {
        public PointRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Point, bool>> GetByIdExpression(int entityId)
        {
            return point => point.Id == entityId;
        }

        protected override IQueryable<Point> IncludeAllChildren(IQueryable<Point> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.EndRoutes)
                    .Include(t => t.StartRoutes)
                : query;
        }
    }
}