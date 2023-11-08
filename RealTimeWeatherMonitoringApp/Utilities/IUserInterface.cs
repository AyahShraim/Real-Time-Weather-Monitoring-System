using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeWeatherMonitoringApp.Utilities
{
    public interface IUserInterface
    {
        string ReadInput();
        void WriteOutput(string message);
        void ClearConsole();
        char ReadKey();
        void SetForegroundColor(ConsoleColor color);
        void ResetColor();
    }
    public class ConsoleUserInterface : IUserInterface
    {
        public string ReadInput() => Console.ReadLine();
        public void WriteOutput(string message) => Console.WriteLine(message);
        public void ClearConsole() => Console.Clear();
        public char ReadKey() => Console.ReadKey().KeyChar;
        public void SetForegroundColor(ConsoleColor color) => Console.ForegroundColor = color;
        public void ResetColor() => Console.ResetColor();
    }

}
