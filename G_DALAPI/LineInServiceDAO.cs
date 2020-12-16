using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineInServiceDAO
    {
        public int LineNumber { get; set; }
        public DateTime StartLineTime { get; set; }
        public int Frequency { get; set; }
        public DateTime EndLineTime { get; set; }


    }
}
