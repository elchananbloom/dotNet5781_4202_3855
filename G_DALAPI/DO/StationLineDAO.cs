using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    //תחנת בקו
    public class StationLineDAO
    {
        public int LineNumber { get; set; }
        public int StationNumber { get; set; }
        //מספר תחנה בקו
        public int NumberStationInLine { get; set; }
        public bool Deleted { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
