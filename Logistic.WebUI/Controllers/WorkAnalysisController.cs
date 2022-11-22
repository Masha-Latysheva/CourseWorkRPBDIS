using System;
using System.Linq;
using System.Threading.Tasks;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Logistic.WebUI.Controllers
{
    [Route("[controller]")]
    public class WorkAnalysisController : Controller
    {
        private readonly ITransportationRepository _transportationRepository;

        public WorkAnalysisController(ITransportationRepository transportationRepository)
        {
            _transportationRepository = transportationRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Stats")]
        public async Task<IActionResult> GetStats(DateTime firstDate, DateTime secondDate)
        {
            secondDate = secondDate.AddHours(23);
            var queryResult = await _transportationRepository.QueryEntities(includeAllChildren: false)
                .Where(x => x.Date >= firstDate && x.Date <= secondDate)
                .Include(t => t.Rate)
                .Include(t => t.Cargo)
                .Select(x => new
                {
                    TotalVolume = x.Cargo.Volume * x.CargoCount,
                    TotalWeight = x.Cargo.Weight * x.CargoCount,
                    TotalSum = x.Rate.CarryingRate * x.Cargo.Weight * x.CargoCount + x.Rate.VolumeRate * x.Cargo.Volume * x.CargoCount
                })
                .ToListAsync();

            if (queryResult.Count == 0)
            {
                return Json("");
            }

            var totalSum = queryResult.Sum(x => x.TotalSum);
            var totalVolume = queryResult.Sum(x => x.TotalVolume);
            var totalWeight = queryResult.Sum(x => x.TotalWeight);

            var totalResult = new
            {
                TotalSum = totalSum,
                TotalVolume = totalVolume,
                TotalWeight = totalWeight
            };

            var serializedResult = JsonConvert.SerializeObject(totalResult);

            return Json(serializedResult);
        }
    }
}