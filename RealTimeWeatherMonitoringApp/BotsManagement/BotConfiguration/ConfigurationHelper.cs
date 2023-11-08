using Microsoft.Extensions.Configuration;

namespace RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration
{
    public static class ConfigurationHelper
    {
        public static string GetBotsConfigurationFilePath()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSetting.json")
                .Build();
            return configuration["BotsConfigurationFileName"];
        }
    }
}
