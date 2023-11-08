using System.Xml;
using System.Xml.Serialization;
using RealTimeWeatherMonitoringApp.WeatherManagement;
using RealTimeWeatherMonitoringApp.Utilities.ResultHnadler;

namespace RealTimeWeatherMonitoringApp.DataFormatManagement
{
    public class XMLFormatStrategy : IProcessInputDataStrategy
    {
        public bool ValidateFormat(string inputData)
        {
            try
            {
                XmlDocument xmlDocument = new();
                xmlDocument.LoadXml(inputData);
                return true;
            }
            catch 
            {
                return false;
            }
        }
      
        public OperationResult ExtractData(string inputData)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WeatherData));
                using (TextReader reader = new StringReader(inputData))
                {
                    var weatherData = (WeatherData)serializer.Deserialize(reader);
                    return OperationResult.SuccessDataMessage("Weather data extracted!", weatherData);
                }
            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Check your input data values!\n{ex.Message}");

            }
        }
    }
}
