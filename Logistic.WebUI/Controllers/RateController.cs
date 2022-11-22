using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Logistic.WebUI.Controllers
{
    public class RateController : BaseController<IRateRepository, Rate>
    {
        public RateController(IRateRepository repository) : base(repository)
        {
        }

        protected override Expression<Func<Rate, bool>> SearchExpression(string searchString)
        {
            return rate => rate.Name.ToLower().Contains(searchString.ToLower().Trim());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var items = await GetSearchQuery(searchString)
                .ToListAsync();

            return View(ToPagedList(items, page));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await Repository.GetEntityById(id);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Rate item)
        {
            await Repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rate item)
        {
            await Repository.Add(item);

            return RedirectToAction(nameof(Index));
        }
    }
}