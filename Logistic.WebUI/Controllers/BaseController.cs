using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Logistic.WebUI.Controllers
{
    public abstract class BaseController<TRepository, TEntity> : Controller
        where TRepository : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly TRepository Repository;

        protected const int PageSize = 10;

        protected BaseController(TRepository repository)
        {
            Repository = repository;
        }

        protected virtual IPagedList<TEntity> ToPagedList(IEnumerable<TEntity> entities, int? currentPage)
        {
            var pageNumber = currentPage ?? 1;
            var pagedItems = entities
                .ToPagedList(pageNumber, PageSize);

            return pagedItems;
        }

        protected virtual IQueryable<TEntity> GetSearchQuery(string searchString)
        {
            var query = Repository.QueryEntities();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query
                    .Where(SearchExpression(searchString))
                    .Take(PageSize);
            }

            return query;
        }

        protected abstract Expression<Func<TEntity, bool>> SearchExpression(string searchString);
    }
}