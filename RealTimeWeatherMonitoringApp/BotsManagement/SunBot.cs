namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    internal class SunBot : IBotFunctionality, IWeatherDataSubscriber
    {
        public float TemperatureThreshold { get; set; }
        public string ActivatedMessage { get; set; } = string.Empty;
        private float CurrentTemperature { get; set; }

        public SunBot(float tempThreshold, string activatedMsg)
        {
            TemperatureThreshold = tempThreshold;
            ActivatedMessage = activatedMsg;
        }
        public bool IsActivate()
        {
            return CurrentTemperature > TemperatureThreshold;
        }

        public void PerformAction()
        {
            bool botHasActivated = IsActivate();
            if (botHasActivated)
            {
                Console.WriteLine("SunBot Activated !");
                Console.WriteLine($"SunBot :{ActivatedMessage}");
            }
        }
        public void Update(float newTemperature, float newHumidity)
        {
            CurrentTemperature = newTemperature;
            PerformAction();
        }
    }
}
