using Logistic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL
{
    public class LogisticContext : DbContext
    {
        public DbSet<Transportation> Transportations { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Point> Points { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<Car> Cars { get; set; }


        public LogisticContext(DbContextOptions<LogisticContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public void DetachEntities<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DetachEntity(entity);
            }
        }

        public void DetachEntity<TEntity>(TEntity entity)
        {
            Entry(entity).State = EntityState.Detached;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Route>()
                .HasOne(route => route.StartPoint)
                .WithMany(point => point.StartRoutes)
                .HasForeignKey(route => route.StartPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Route>()
                .HasOne(route => route.EndPoint)
                .WithMany(point => point.EndRoutes)
                .HasForeignKey(route => route.EndPointId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
