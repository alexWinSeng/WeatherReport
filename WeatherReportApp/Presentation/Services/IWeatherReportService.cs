using Presentation.Models;
using Presentation.ResquestResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public interface IWeatherReportService
    {
        Task<Result<WeatherReport>> GetWeatherConditions(Dictionary<string, string> queryParams);
    }
}