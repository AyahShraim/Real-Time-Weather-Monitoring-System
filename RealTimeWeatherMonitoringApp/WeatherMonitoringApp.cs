using RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration;
using RealTimeWeatherMonitoringApp.BotsManagement;
using RealTimeWeatherMonitoringApp.WeatherManagement;
using RealTimeWeatherMonitoringApp.DataFormatManagement;
using RealTimeWeatherMonitoringApp.Utilities;

namespace RealTimeWeatherMonitoringApp
{
    public class WeatherMonitoringApp
    {
        private readonly WeatherStation _weatherStation;
        private readonly IOutputWriter _outputWriter;
        private readonly IUserInterface _userInterface;

        public WeatherMonitoringApp(WeatherStation weatherStation, IOutputWriter outputWriter, IUserInterface userInterface)
        {
            _weatherStation = weatherStation;
            _outputWriter = outputWriter;
            _userInterface = userInterface;
        }
        public void Run()
        {
            SetConfiguration();
            StartApplication();
        }
        internal void SetConfiguration()
        {
            string FileName = ConfigurationHelper.GetBotsConfigurationFilePath();
            var botConfigurationResult = BotConfigurationRepository.LoadBotsConfiguration(FileName);
            if (botConfigurationResult.IsSuccess)
            {
                var botsConfiguration = (Dictionary<BotName, BotConfigurationModel>)botConfigurationResult.Data;
                foreach (var Bot in botsConfiguration.Where(config => config.Value.Enabled))
                {
                    var tempThreshold = Bot.Value.TemperatureThreshold;
                    var humidityThreshold = Bot.Value.HumidityThreshold;
                    var msg = Bot.Value.Message;
                    if (Bot.Key.Equals(BotName.RainBot)) _weatherStation.AddSubscriber(new RainBot(humidityThreshold, msg, _outputWriter));
                    if (Bot.Key.Equals(BotName.SunBot)) _weatherStation.AddSubscriber(new SunBot(tempThreshold, msg, _outputWriter));
                    if (Bot.Key.Equals(BotName.SunBot)) _weatherStation.AddSubscriber(new SnowBot(tempThreshold, msg, _outputWriter));
                }
            }
            else
            {
                _outputWriter.WriteLine(botConfigurationResult.Message);
                Environment.Exit(0);
            }
        }

        private void StartApplication()
        {
            _userInterface.WriteOutput("\n********************************");
            _userInterface.WriteOutput("Enter weather data:");
            string inputData = _userInterface.ReadInput();
            DataFormatHandler(inputData);
        }
        private void DataFormatHandler(string inputData)
        {
            List<IProcessInputDataStrategy> dataFormatProcessStrategies = new()
            {
                new JsonFormatStrategy(),
                new XMLFormatStrategy(),
            };
            var InputFormatStrategy = dataFormatProcessStrategies
                .FirstOrDefault(strategy => strategy.ValidateFormat(inputData));
            if (InputFormatStrategy == null)
            {
                _userInterface.WriteOutput("Not valid input format");
                StartApplication();
            }
            var weatherDataExtraction = InputFormatStrategy.ExtractData(inputData);
            if (!weatherDataExtraction.IsSuccess)
            {
                _userInterface.WriteOutput(weatherDataExtraction.Message);
                StartApplication();
            }
            var newData = (WeatherData)weatherDataExtraction.Data;
            SetWeatherStationState(newData);
        }
        private void SetWeatherStationState(WeatherData newState)
        {
            _userInterface.WriteOutput($"Location:{newState.Location}");
            _weatherStation.SetWeatherStationState(newState.Temperature, newState.Humidity);
            ReadNewData();
        }
        private void ReadNewData()
        {
            SetUpConsole();
            char key = _userInterface.ReadKey();
            if (char.ToLower(key) == 'f')
                Environment.Exit(0);
            StartApplication();
        }
        private void SetUpConsole() 
        {
            _userInterface.SetForegroundColor(ConsoleColor.Green);
            _userInterface.WriteOutput("\npress f key to finish - any other key to enter new data");
            _userInterface.ResetColor();
        }
    }
}
