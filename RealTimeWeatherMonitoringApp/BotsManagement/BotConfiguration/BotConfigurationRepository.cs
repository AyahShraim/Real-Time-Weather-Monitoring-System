using RealTimeWeatherMonitoringApp.ResultHandler;
using System.Text.Json;

namespace RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration
{
    public class BotConfigurationRepository
    {    
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public OperationResult LoadBotsConfiguration(string configurationFilePath)
        {
            var botsConfiguration = new Dictionary<BotName, BotConfiguration>(); 
            try
            {
                var jsonString = File.ReadAllText(configurationFilePath);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    botsConfiguration = JsonSerializer.Deserialize<Dictionary<BotName, BotConfiguration>>(jsonString, _options);
                    return OperationResult.SuccessDataMessage("Bots settings loaded successfully", botsConfiguration);
                }
                return OperationResult.FailureResult("Configuration file is Empty!");    
            }
            catch(Exception ex)
            {
                return OperationResult.FailureDataMessage($"Error on configuration file : {ex.Message}", botsConfiguration);
            }     
        }
    }
}
