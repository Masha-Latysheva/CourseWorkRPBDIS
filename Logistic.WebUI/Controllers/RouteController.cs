using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebUI.Controllers
{
    public class RouteController : BaseController<IRouteRepository, Route>
    {
        private readonly IPointRepository _pointRepository;

        public RouteController(IRouteRepository repository, IPointRepository pointRepository) : base(repository)
        {
            _pointRepository = pointRepository;
        }

        protected override Expression<Func<Route, bool>> SearchExpression(string searchString)
        {
            return route => route.Name.ToLower().Contains(searchString.ToLower().Trim());
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

            var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            ViewBag.Points = points;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Route item)
        {
            if (!ModelState.IsValid)
            {
                var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                    .ToListAsync();
                ViewBag.Points = points;
                return View(item);
            }

            await Repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            ViewBag.Points = points;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Route item)
        {
            if (!ModelState.IsValid)
            {
                var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                    .ToListAsync();
                ViewBag.Points = points;
                return View(item);
            }

            await Repository.Add(item);

            return RedirectToAction(nameof(Index));
        }
    }
}