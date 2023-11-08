namespace RealTimeWeatherMonitoringApp.Utilities
{
    public interface IOutputWriter
    {
        void WriteLine(string message);
    }
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
