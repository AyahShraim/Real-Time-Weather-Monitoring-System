namespace RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration

{
    public class BotConfiguration
    {
        public bool Enabled {  get; set; }
        public float humidityThreshold {  get; set; }
        public float temperatureThreshold { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
