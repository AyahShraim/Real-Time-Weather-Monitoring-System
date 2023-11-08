using RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeWeatherMonitoringApp.Tests.BotsTests
{
    public class BotConfigurationRepositoryShould
    {
        [Fact]
        public void LoadBotsConfiguration_ShouldReturnSuccess()
        {
            string filePath = ConfigurationHelper.GetBotsConfigurationFilePath();
            var result = BotConfigurationRepository.LoadBotsConfiguration(filePath);
            var expectedMessage = "Bots settings loaded successfully";
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedMessage,result.Message);
            
        }
    }
}
