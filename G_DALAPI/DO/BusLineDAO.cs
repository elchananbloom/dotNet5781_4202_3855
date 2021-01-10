using DALAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    //קו אוטובוס
    public class BusLineDAO
    {
        //מזהה קו אוטובוס
   
        public int CurrentSerialNB { get; set; }
        public int LineNumber { get; set; }
        public Area Area { get; set; }
        public int FirstStationNumber { get; set; }
        public int LastStationNumber { get; set; }
        //public List<int> LineStationNumbers { get; set; }
        //public List<StationLineDAO> LineStations { get; set; }
        public bool Deleted { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
