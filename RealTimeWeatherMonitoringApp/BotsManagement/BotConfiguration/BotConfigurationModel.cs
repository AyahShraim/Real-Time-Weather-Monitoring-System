namespace RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration
{
    public class BotConfigurationModel
    {
        public bool Enabled {  get; set; }
        public float HumidityThreshold {  get; set; }
        public float TemperatureThreshold { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
