using RealTimeWeatherMonitoringApp.BotsManagement;

namespace RealTimeWeatherMonitoringApp.WeatherManagement
{
    public interface IWeatherDataPublisher
    {
        void AddSubscriber(IWeatherDataSubscriber subscriber);
        void RemoveSubscriber(IWeatherDataSubscriber subscriber);
        void NotifySubscriber();
    }
}
