
using RealTimeWeatherMonitoringApp.Utilities;

namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    public class SnowBot : IWeatherDataSubscriber, IBotFunctionality
    {
        public float TemperatureThreshold { get; set; }
        public string ActivatedMessage { get; set; } = string.Empty;
        internal float CurrentTemperature { get; set; }
        internal IOutputWriter _outputWriter;

        public SnowBot(float tempThreshold, string activatedMsg, IOutputWriter outputWriter)
        {
            TemperatureThreshold = tempThreshold;
            ActivatedMessage = activatedMsg;
            _outputWriter = outputWriter;
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
                _outputWriter.WriteLine("SnowBot Activated !");
                _outputWriter.WriteLine($"SnowBot :{ActivatedMessage}");
            }
        }
        public void Update(float newTemperature, float newHumidity)
        {
            CurrentTemperature = newTemperature;
            PerformAction();
        }
    }
}
