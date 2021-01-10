using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DataSource
{
    public static class Configuration
    {
        private static int serialBusInTravel = 0;
        private static int serialBusLine = 0;
        private static int serialLineInService = 0;

        public static int SerialBusInTravel => ++serialBusInTravel;
        public static int SerialBusLine => ++serialBusLine;
        public static int SerialLineInService => ++serialLineInService;
    }
}
