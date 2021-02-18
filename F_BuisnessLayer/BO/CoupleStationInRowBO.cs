using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CoupleStationInRowBO
    {
        public int StationNumberOne { get; set; }
        public int StationNumberTwo { get; set; }
        public int Distance { get; set; }
        public TimeSpan AverageTravelTime { get; set; }
    }
}
