using Logistic.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransportationRepository _transportationRepository;

        public HomeController(ITransportationRepository transportationRepository)
        {
            _transportationRepository = transportationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;
            var yesterday = now.AddDays(-1);

            var todayRoutesCount = await _transportationRepository.QueryEntities(includeAllChildren: false)
                .CountAsync(t => t.Date.Day == now.Day && t.Date.Month == now.Month && t.Date.Year == now.Year);
            var yesterdayRoutesCount = await _transportationRepository.QueryEntities(includeAllChildren: false)
                .CountAsync(t => t.Date.Day == yesterday.Day && t.Date.Month == yesterday.Month && t.Date.Year == yesterday.Year);
            var monthRoutesCount = await _transportationRepository.QueryEntities(includeAllChildren: false)
                .CountAsync(t => t.Date.Month == now.Month && t.Date.Year == now.Year);

            var result = new DashboardViewModel
            {
                TodayRoutesCount = todayRoutesCount,
                MonthRoutesCount = monthRoutesCount,
                YesterdayRoutesCount = yesterdayRoutesCount
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RoutesGraphicData()
        {
            var now = DateTime.Now.AddDays(-13);

            var result = new GraphicViewModel
            {
                Days = new List<string>(),
                Counts = new List<int>()
            };

            for (var i = 0; i < 14; i++)
            {
                var currData = await _transportationRepository.QueryEntities(includeAllChildren: false)
                    .CountAsync(t => t.Date.Day == now.Day && t.Date.Month == now.Month && t.Date.Year == now.Year);

                result.Counts.Add(currData);
                result.Days.Add(now.ToString("d"));
                now = now.AddDays(1);
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> RoutesDetails(int param)
        {
            var query = _transportationRepository.QueryEntities();

            var now = DateTime.Now;
            var yesterday = now.AddDays(-1);
            query = param switch
            {
                2 => query
                    .Where(t => t.Date.Day == now.Day && t.Date.Month == now.Month && t.Date.Year == now.Year),
                1 => query
                    .Where(t => t.Date.Day == yesterday.Day && t.Date.Month == yesterday.Month && t.Date.Year == yesterday.Year),
                3 => query
                    .Where(t => t.Date.Month == now.Month && t.Date.Year == now.Year),
                _ => query
            };

            var result = await query
                .AsNoTracking()
                .ToListAsync();

            return PartialView(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
