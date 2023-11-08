using Microsoft.Extensions.Configuration;
using RealTimeWeatherMonitoringApp.BotsManagement;
using RealTimeWeatherMonitoringApp.BotsManagement.BotConfiguration;
using RealTimeWeatherMonitoringApp.DataFormatManagement;
using RealTimeWeatherMonitoringApp.WeatherManagement;

WeatherStation weatherStation = new();
SetConfiguration();
StartApplication();

void SetConfiguration()
{
    var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSetting.json")
    .Build();
    string FileName = configuration["BotsConfigurationFileName"];
    var botConfigurationResult = BotConfigurationRepository.LoadBotsConfiguration(FileName);
    if (botConfigurationResult.IsSuccess)
    {
        var botsConfiguration = (Dictionary<BotName, BotConfigurationModel>)botConfigurationResult.Data;
        foreach (var Bot in botsConfiguration.Where(config => config.Value.Enabled))
        {
            var tempThreshold = Bot.Value.TemperatureThreshold;
            var humidityThreshold = Bot.Value.HumidityThreshold;
            var msg = Bot.Value.Message;
            if(Bot.Key.Equals(BotName.RainBot))  weatherStation.AddSubscriber(new RainBot(humidityThreshold,msg));
            if(Bot.Key.Equals(BotName.SunBot))  weatherStation.AddSubscriber(new SunBot(tempThreshold, msg));
            if(Bot.Key.Equals(BotName.SunBot)) weatherStation.AddSubscriber(new SnowBot(tempThreshold, msg));
        }
    }
    else
    {
        Console.WriteLine(botConfigurationResult.Message);
        Environment.Exit(0);
    }
};   
void StartApplication()
{
    Console.WriteLine("\n********************************");
    Console.WriteLine("Enter weather data:");
    string inputData = Console.ReadLine() ?? string.Empty;
    DataFormatHandler(inputData);
}
void DataFormatHandler(string inputData)
{
    List<IProcessInputDataStrategy> dataFormatProcessStrategies = new()
    {
        new JsonFormatStrategy(),
        new XMLFormatStrategy(),
    };
    var InputFormatStrategy = dataFormatProcessStrategies
        .FirstOrDefault(strategy => strategy.ValidateFormat(inputData).IsSuccess);
    if(InputFormatStrategy == null)
    {
        Console.WriteLine("Not valid input format");
        StartApplication();
    }
    var weatherDataExtraction = InputFormatStrategy.ExtractData(inputData);
    if (!weatherDataExtraction.IsSuccess)
    {
        Console.WriteLine(weatherDataExtraction.Message);
        StartApplication();
    }
    var newData = (WeatherData)weatherDataExtraction.Data;
    SetWeatherStationState(newData);
}
void SetWeatherStationState(WeatherData newState)
{
    Console.WriteLine($"Location:{newState.Location}");
    weatherStation.SetWeatherStationState(newState.Temperature, newState.Humidity);
    ReadNewData();
}
void ReadNewData()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\npress f key to finish - any other key to enter new data");
    Console.ResetColor();
    char key = Console.ReadKey().KeyChar;
    if(char.ToLower(key) == 'f')
        Environment.Exit(0);
    StartApplication();
}

