using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusDAO
    {
        public string LicenseNumber { get; set; }
        public DateTime StartOfWork { get; set; }
        public int TotalKms { get; set; }
        public int Fuel { get; set; }
        public int Maintnance { get; set; }
        public Status Status { get; set; }
        public bool Deleted { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
        //....

    }
}
