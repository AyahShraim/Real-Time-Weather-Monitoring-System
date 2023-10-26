namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    public interface IWeatherDataSubscriber
    {
        public void Update(float newTemperature, float newHumidity);
    }
}
