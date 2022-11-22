using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebUI.Controllers
{
    public class TransportationController : BaseController<ITransportationRepository, Transportation>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly ICargoRepository _cargoRepository;
        private readonly ICarRepository _carRepository;

        public TransportationController(
            ITransportationRepository repository,
            IDriverRepository driverRepository,
            IRateRepository rateRepository,
            IRouteRepository routeRepository,
            ICargoRepository cargoRepository,
            ICarRepository carRepository) : base(repository)
        {
            _driverRepository = driverRepository;
            _rateRepository = rateRepository;
            _routeRepository = routeRepository;
            _cargoRepository = cargoRepository;
            _carRepository = carRepository;
        }

        protected override Expression<Func<Transportation, bool>> SearchExpression(string searchString)
        {
            return transportation => transportation.Route.Name.ToLower().Contains(searchString.ToLower().Trim());
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

            await FillViewBag();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Transportation item)
        {
            await Repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillViewBag();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transportation item)
        {
            await Repository.Add(item);

            return RedirectToAction(nameof(Index));
        }

        private async Task FillViewBag()
        {
            var cars = await _carRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            var drivers = await _driverRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            var rates = await _rateRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            var cargos = await _cargoRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            var routes = await _routeRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();

            ViewBag.Cars = cars;
            ViewBag.Drivers = drivers;
            ViewBag.Rates = rates;
            ViewBag.Cargos = cargos;
            ViewBag.Routes = routes;
        }
    }
}