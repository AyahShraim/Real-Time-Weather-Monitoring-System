using RealTimeWeatherMonitoringApp.Utilities.ResultHnadler;
using System.Text.Json;

namespace RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration
{
    public class BotConfigurationRepository
    {    
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public static OperationResult LoadBotsConfiguration(string configurationFilePath)
        {
            var botsConfiguration = new Dictionary<BotName, BotConfigurationModel>(); 
            try
            {
                var jsonString = File.ReadAllText(configurationFilePath);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    botsConfiguration = JsonSerializer.Deserialize<Dictionary<BotName, BotConfigurationModel>>(jsonString, _options);
                    return OperationResult.SuccessDataMessage("Bots settings loaded successfully", botsConfiguration);
                }
                return OperationResult.FailureResult("Configuration file is Empty!");    
            }
            catch(Exception ex)
            {
                return OperationResult.FailureDataMessage($"Error on configuration file : {ex.Message}", botsConfiguration);
            }     
        }
        internal OperationResult LoadBotsConfigurationWrapper(string fileName)
        {
            return BotConfigurationRepository.LoadBotsConfiguration(fileName);
        }
    }
}
