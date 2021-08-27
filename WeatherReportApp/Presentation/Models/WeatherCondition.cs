using Presentation.ResquestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class WeatherReport : BaseResponse
    {
        public bool Success { get; set; }
        public Record Records { get; set; }
    }

    public class Record
    {
        public string DatasetDescription { get; set; }
        public List<LocationItem> Location { get; set; }
    }

    public class LocationItem
    {
        public string LocationName { get; set; }
        public List<WeatherElement> WeatherElement { get; set; }
    }

    public class WeatherElement
    {
        public string ElementName { get; set; }
        public List<WeatherCondition> Time { get; set; }
    }

    public class WeatherCondition
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Parameter Parameter { get; set; }
    }

    public class Parameter
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterUnit { get; set; }
    }
}
