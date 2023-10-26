using RealTimeWeatherMonitoringApp.ResultHandler;

namespace RealTimeWeatherMonitoringApp.DataFormatManagement
{
    public interface IProcessInputDataStrategy
    {
       
        OperationResult ValidateFormat(string inputData);
        public OperationResult ExtractData(string inputData);

     
        public record WeatherData(string Location, float Temperature, float Humidity);
    }
}
