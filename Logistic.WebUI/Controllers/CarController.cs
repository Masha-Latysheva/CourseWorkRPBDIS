using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebUI.Controllers
{
    public class CarController : BaseController<ICarRepository, Car>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public CarController(ICarRepository repository, IOrganizationRepository organizationRepository) : base(repository)
        {
            _organizationRepository = organizationRepository;
        }

        protected override Expression<Func<Car, bool>> SearchExpression(string searchString)
        {
            return car => car.Organization.Name.ToLower().Contains(searchString.ToLower().Trim());
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

            var organizations = await _organizationRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            ViewBag.Organizations = organizations;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Car item)
        {
            await Repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var organizations = await _organizationRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            ViewBag.Organizations = organizations;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car item)
        {
            await Repository.Add(item);

            return RedirectToAction(nameof(Index));
        }
    }
}