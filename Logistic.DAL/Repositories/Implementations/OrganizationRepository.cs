using System;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Organization, bool>> GetByIdExpression(int entityId)
        {
            return organization => organization.Id == entityId;
        }

        protected override IQueryable<Organization> IncludeAllChildren(IQueryable<Organization> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.Cars)
                : query;
        }
    }
}