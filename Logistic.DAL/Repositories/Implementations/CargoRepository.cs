using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Repositories.Implementations
{
    public class CargoRepository : BaseRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Cargo, bool>> GetByIdExpression(int entityId)
        {
            return cargo => cargo.Id == entityId;
        }

        protected override IQueryable<Cargo> IncludeAllChildren(IQueryable<Cargo> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.Transportations)
                : query;
        }
    }
}
