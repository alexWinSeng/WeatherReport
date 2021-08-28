using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Constants;
using Presentation.DataAccess;
using Presentation.DataAccess.Models;
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
        private readonly Weather_Report_DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger
            , IWeatherReportService weatherReportService
            , Weather_Report_DataContext dataContext)
        {
            _logger = logger;
            _weatherReportService = weatherReportService;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var queryParams = new Dictionary<string, string>
            {
                { "Authorization", "CWB-BC115EF1-9CE2-4131-B93E-CCE15EB4A7BD" },
                { "format", "JSON" },
                { "locationName", "宜蘭縣" },
                { "elementName", "" },
                { "sort", "time" },
            };
            var result = await _weatherReportService.GetWeatherConditions(queryParams);

            if (result.ErrorCode == ResponseCodes.Success && result.Data.Success)
            {
                var viewModel = result.Data;
                await InsertToDb(viewModel);
                return View(viewModel);
            }
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

        private async Task InsertToDb(WeatherReport weatherReport)
        {
            var location = new Location
            {
                LocationName = weatherReport.Records.Location[0].LocationName
            };

            var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                await _dataContext.Location.AddAsync(location);
                await _dataContext.SaveChangesAsync();

                var locationId = location.Id;

                foreach (var weatherEle in weatherReport.Records.Location[0].WeatherElement)
                {
                    var weatherElemnt = new DataAccess.Models.WeatherElement
                    {
                        LocationId = locationId,
                        ElementName = weatherEle.ElementName
                    };

                    await _dataContext.WeatherElement.AddAsync(weatherElemnt);
                    await _dataContext.SaveChangesAsync();

                    var wetherElementId = weatherElemnt.Id;
                    foreach (var time in weatherEle.Time)
                    {
                        var weatherCondition = new DataAccess.Models.WeatherCondition
                        {
                            ElementId = wetherElementId,
                            StartTime = time.StartTime,
                            EndTime = time.EndTime,
                            ParameterName = time.Parameter.ParameterName,
                            ParameterValue = time.Parameter.ParameterValue ?? time.Parameter.ParameterUnit ?? ""
                        };
                        await _dataContext.WeatherCondition.AddAsync(weatherCondition);
                    }
                }
                await tran.CommitAsync();
            }
            catch
            {
                await tran.RollbackAsync();
            }
        }
    }
}
