using System;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Route, bool>> GetByIdExpression(int entityId)
        {
            return route => route.Id == entityId;
        }

        protected override IQueryable<Route> IncludeAllChildren(IQueryable<Route> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.StartPoint)
                    .Include(t => t.EndPoint)
                    .Include(t => t.Transportations)
                : query;
        }
    }
}