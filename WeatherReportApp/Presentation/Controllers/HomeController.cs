using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using Presentation.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherReportService _weatherReportService;

        public HomeController(ILogger<HomeController> logger
            , IWeatherReportService weatherReportService)
        {
            _logger = logger;
            _weatherReportService = weatherReportService;
        }

        public IActionResult Index()
        {
            var queryParams = new Dictionary<string, string>
            {
                { "Authorization", "CWB-BC115EF1-9CE2-4131-B93E-CCE15EB4A7BD" },
                { "format", "JSON" },
                { "locationName", "宜蘭縣" },
                { "elementName", "" },
                { "sort", "time" },
            };
            var result = _weatherReportService.GetWeatherConditions(queryParams);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
