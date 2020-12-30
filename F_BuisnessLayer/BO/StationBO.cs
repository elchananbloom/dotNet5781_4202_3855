﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationBO
    {
        public int StationNumber { get; set; }
        public string StationName { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<BusLineBO> BusLinesInStation { get; set; }
    }
}
