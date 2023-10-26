using RealTimeWeatherMonitoringApp.DataFormatManagement;
using RealTimeWeatherMonitoringApp.WeatherManagement;

WeatherStation weatherStation = new();
StartApplication();


void StartApplication()
{
    Console.WriteLine("\n********************************");
    WeatherStation weatherStation = new WeatherStation();
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
    Console.WriteLine(newState.ToString());
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