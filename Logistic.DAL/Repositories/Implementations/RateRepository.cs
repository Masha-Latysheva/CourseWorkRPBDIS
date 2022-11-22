using System;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class RateRepository : BaseRepository<Rate>, IRateRepository
    {
        public RateRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Rate, bool>> GetByIdExpression(int entityId)
        {
            return rate => rate.Id == entityId;
        }

        protected override IQueryable<Rate> IncludeAllChildren(IQueryable<Rate> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.Transportations)
                : query;
        }
    }
}