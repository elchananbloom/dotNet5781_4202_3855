using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLineBO
    {
        //public int CurrentSerialNB { get; set; }
        public int LineNumber { get; set; }
        public Area Area { get; set; }
        public StationBO FirstStation { get; set; }
        public StationBO LastStation { get; set; }
        public List<StationLineBO> StationLines { get; set; }
        //public List<int> LineStationNumbers { get; set; }
        //public List<StationLineDAO> LineStations { get; set; }
        public bool Deleted { get; set; }
    }
}
