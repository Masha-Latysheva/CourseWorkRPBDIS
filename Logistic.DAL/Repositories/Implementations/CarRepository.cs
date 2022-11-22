using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logistic.DAL.Repositories.Implementations
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(LogisticContext context) : base(context)
        {
        }

        protected override Expression<Func<Car, bool>> GetByIdExpression(int entityId)
        {
            return car => car.Id == entityId;
        }

        protected override IQueryable<Car> IncludeAllChildren(IQueryable<Car> query, bool includeAllChildren)
        {
            return includeAllChildren
                ? query
                    .Include(t => t.Organization)
                    .Include(t => t.Transportations)
                : query;
        }
    }
}
