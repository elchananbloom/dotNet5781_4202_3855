using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineInServiceBO
    {
        //public int LineInServiceSerialNB { get; set; }
        public int LineNumber { get; set; }
        public TimeSpan StartLineTime { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan EndLineTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
}
