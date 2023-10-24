using RealTimeWeatherMonitoringApp.BotsManagement;

namespace RealTimeWeatherMonitoringApp.WeatherManagement
{
    public class WeatherStation : IWeatherDataPublisher
    {
        private string Location { get; set; } = String.Empty;
        private float Temperature { get; set; }
        private float Humidity { get; set; }
        private List<IWeatherDataSubscriber> Subscribers { get; set; } = new();

        public void SetWeatherStationState(string location, float temperature, float humidity)
        {
            Location = location;
            Temperature = temperature;
            Humidity = humidity;
            NotifySubscriber();
        }
        public void NotifySubscriber()
        {
            Subscribers.ForEach(subscriber => subscriber.Update(Temperature, Humidity));
        }
        public void RemoveSubscriber(IWeatherDataSubscriber subscriber)
        {
            Subscribers.Remove(subscriber);
        }
        public void AddSubscriber(IWeatherDataSubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }
    }
}
