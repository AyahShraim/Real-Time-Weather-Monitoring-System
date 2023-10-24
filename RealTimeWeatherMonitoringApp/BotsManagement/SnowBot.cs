
namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    internal class SnowBot : IWeatherDataSubscriber, IBotFunctionality
    {
        public float TemperatureThreshold { get; set; }
        public string ActivatedMessage { get; set; } = string.Empty;
        private float CurrentTemperature { get; set; }

        public SnowBot(float tempThreshold, string activatedMsg)
        {
            TemperatureThreshold = tempThreshold;
            ActivatedMessage = activatedMsg;
        }
        public bool IsActivate()
        {
            return CurrentTemperature < TemperatureThreshold;
        }

        public void PerformAction()
        {
            bool botHasActivated = IsActivate();
            if (botHasActivated)
            {
                Console.WriteLine("SnowBot Activated !");
                Console.WriteLine($"SnowBot :{ActivatedMessage}");
            }
        }
        public void Update(float newTemperature, float newHumidity)
        {
            CurrentTemperature = newTemperature;
            PerformAction();
        }
    }
}
