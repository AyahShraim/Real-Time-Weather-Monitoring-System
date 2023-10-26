using System.Xml.Serialization;

namespace RealTimeWeatherMonitoringApp.WeatherManagement
{
    [XmlRoot("WeatherData")]
    public record WeatherData
    {
        public  string Location { get; init; } = String.Empty;
        public  float Temperature { get; init; }
        public  float Humidity { get; init; }
    };
   
}
