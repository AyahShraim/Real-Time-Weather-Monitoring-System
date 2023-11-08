using RealTimeWeatherMonitoringApp;
using RealTimeWeatherMonitoringApp.Utilities;
using RealTimeWeatherMonitoringApp.WeatherManagement;

WeatherMonitoringApp weatherMonitoringApp = new WeatherMonitoringApp(new WeatherStation(),new ConsoleOutputWriter(), new ConsoleUserInterface());
weatherMonitoringApp.Run();


  

