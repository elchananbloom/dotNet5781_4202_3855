using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationLineBO
    {
        public int LineNumber { get; set; }
        public StationBO Station { get; set; }
        //מספר תחנה בקו
        public int NumberStationInLine { get; set; }
        public bool Deleted { get; set; }
    }
}
