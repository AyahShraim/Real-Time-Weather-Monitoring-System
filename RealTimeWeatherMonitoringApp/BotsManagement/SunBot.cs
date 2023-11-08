using RealTimeWeatherMonitoringApp.Utilities;

namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    public class SunBot : IBotFunctionality, IWeatherDataSubscriber
    {
        public float TemperatureThreshold { get; set; }
        public string ActivatedMessage { get; set; } = string.Empty;
        internal float CurrentTemperature { get; set; }
        internal IOutputWriter _outputWriter;
        public SunBot(float tempThreshold, string activatedMsg, IOutputWriter outputWriter)
        {
            TemperatureThreshold = tempThreshold;
            ActivatedMessage = activatedMsg;
            _outputWriter = outputWriter;
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
                _outputWriter.WriteLine("SunBot Activated !");
                _outputWriter.WriteLine($"SunBot :{ActivatedMessage}");
            }
        }
        public void Update(float newTemperature, float newHumidity)
        {
            CurrentTemperature = newTemperature;
            PerformAction();
        }
    }
}
