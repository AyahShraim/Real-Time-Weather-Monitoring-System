using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealTimeWeatherMonitoringApp.Utilities.ResultHnadler;
using RealTimeWeatherMonitoringApp.WeatherManagement;

namespace RealTimeWeatherMonitoringApp.DataFormatManagement
{
    public class JsonFormatStrategy : IProcessInputDataStrategy
    {
        public bool ValidateFormat(string inputData)
        {
            try
            {
                JObject jsonInputObject = JObject.Parse(inputData);
                return true;
            }
            catch (Exception) 
            {
                return false;
            }
        }
        public OperationResult ExtractData(string inputData)
        {
            try
            {
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(inputData);
                return OperationResult.SuccessDataMessage("Weather data extracted!", weatherData);

            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Check your input data values!\n{ex.Message}");

            }
        }
    }
}
