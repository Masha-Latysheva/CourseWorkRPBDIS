using System;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class TransportationRepository : BaseRepository<Transportation>, ITransportationRepository
    {
        public TransportationRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Transportation, bool>> GetByIdExpression(int entityId)
        {
            return transportation => transportation.Id == entityId;
        }

        protected override IQueryable<Transportation> IncludeAllChildren(IQueryable<Transportation> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.Car)
                        .ThenInclude(car => car.Organization)
                    .Include(t => t.Cargo)
                    .Include(t => t.Driver)
                    .Include(t => t.Route)
                    .Include(t => t.Rate)
                : query;
        }
    }
}