using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeWeatherMonitoringApp.BotsManagement
{
    internal interface IBotFunctionality
    {
        bool IsActivate();
        void PerformAction();
    }
}
