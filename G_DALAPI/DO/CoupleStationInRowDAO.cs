using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class CoupleStationInRowDAO
    {
        public int StationNumberOne { get; set; }
        public int StationNumberTwo { get; set; }
        public int Distance { get; set; }
        public TimeSpan AverageTravelTime { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
