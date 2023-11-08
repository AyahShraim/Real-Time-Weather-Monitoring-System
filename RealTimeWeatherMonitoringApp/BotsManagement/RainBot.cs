namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    internal class RainBot : IWeatherDataSubscriber, IBotFunctionality
    {
        public float HumidityThreshold { get; set; }
        public string ActivatedMessage { get; set; } = string.Empty;
        public float CurrentHumidity { get; set; }

        public RainBot(float humidityThreshold, string activatedMsg)
        {
            HumidityThreshold = humidityThreshold;
            ActivatedMessage = activatedMsg;
        }
        public bool IsActivate()
        {
            return CurrentHumidity > HumidityThreshold;
        }

        public void PerformAction()
        {
            bool botHasActivated = IsActivate();
            if (botHasActivated)
            {
                Console.WriteLine("RainBot Activated !");
                Console.WriteLine($"RainBot :{ActivatedMessage}");
            }
        }
        public void Update(float newTemperature, float newHumidity)
        {
            CurrentHumidity = newHumidity;
            PerformAction();
        }
    }
}
