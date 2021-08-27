using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Presentation.Models;
using Presentation.ResquestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class WeatherReportService : IWeatherReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherReportService(HttpClient httpClient
            , IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Result<WeatherReport>> GetWeatherConditions(Dictionary<string, string> queryParams)
        {
            var uri = QueryHelpers.AddQueryString(_configuration["BaseUrl"], queryParams);
            var response = await _httpClient.GetAsync(uri);
            string responseString = await response.Content.ReadAsStringAsync();

            var result = new Result<WeatherReport>();
            if (response.IsSuccessStatusCode)
            {
                var weatherReport = JsonConvert.DeserializeObject<WeatherReport>(responseString);
                var json = JsonConvert.SerializeObject(weatherReport);
                result.Data = weatherReport;
            }
            else
            {
                result.ErrorCode = Constants.ResponseCodes.SystemError;
                result.Message = $"Http errorCode : {response.StatusCode} ErrorMessage: {responseString}";
            }
            return result;
        }
    }
}
