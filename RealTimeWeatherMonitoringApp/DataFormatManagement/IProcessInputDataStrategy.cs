using RealTimeWeatherMonitoringApp.Utilities.ResultHnadler;

namespace RealTimeWeatherMonitoringApp.DataFormatManagement
{
    public interface IProcessInputDataStrategy
    {
        bool ValidateFormat(string inputData);
        public OperationResult ExtractData(string inputData);
    }
}
