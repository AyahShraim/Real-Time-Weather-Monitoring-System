using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealTimeWeatherMonitoringApp.ResultHandler;
using RealTimeWeatherMonitoringApp.WeatherManagement;

namespace RealTimeWeatherMonitoringApp.DataFormatManagement
{
    public class JsonFormatStrategy : IProcessInputDataStrategy
    {
        public OperationResult ValidateFormat(string inputData)
        {
            try
            {
                JObject jsonInputObject = JObject.Parse(inputData);
                return OperationResult.SuccessResult("Validated json format");
            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Not valid json {ex.Message}");
            }
        }
        public OperationResult ExtractData(string inputData)
        {
            try
            {
                WeatherData weatherData = new();
                weatherData = JsonConvert.DeserializeObject<WeatherData>(inputData);
                return OperationResult.SuccessDataMessage("Weather data extracted!", weatherData);

            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Check your input data values!\n{ex.Message}");

            }
        }
    }
}
