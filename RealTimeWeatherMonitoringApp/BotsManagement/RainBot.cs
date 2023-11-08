using RealTimeWeatherMonitoringApp.Utilities;

namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    public class RainBot : IWeatherDataSubscriber, IBotFunctionality
    {
        public float HumidityThreshold { get; set; }
        public string ActivatedMessage { get; set; } = string.Empty;
        internal float CurrentHumidity { get; set; }
        internal IOutputWriter _outputWriter;

        public RainBot(float humidityThreshold, string activatedMsg,IOutputWriter outputWriter)
        {
            HumidityThreshold = humidityThreshold;
            ActivatedMessage = activatedMsg;
            _outputWriter = outputWriter;
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
                _outputWriter.WriteLine("RainBot Activated !");
                _outputWriter.WriteLine($"RainBot: {ActivatedMessage}");
            }
        }
        public void Update(float newTemperature, float newHumidity)
        {
            CurrentHumidity = newHumidity;
            PerformAction();
        }
    }
}
