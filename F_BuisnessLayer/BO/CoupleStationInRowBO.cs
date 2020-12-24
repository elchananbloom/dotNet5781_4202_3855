using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CoupleStationInRowBO
    {
        public StationBO StationOne { get; set; }
        public StationBO StationTwo { get; set; }
        public int Distance { get; set; }
        public TimeSpan AverageTravelTime { get; set; }
    }
}
