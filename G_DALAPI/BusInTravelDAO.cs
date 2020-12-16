using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusInTravelDAO
    {
        //מזהה אוטובוס בנסיעה
        private static int serial = 1;
        //compound key
        public string LicenseNumber { get; set; }
        //מזהה קו שבביצוע
        public int Line { get; set; }
        //שעת יציאה לקו הפורמלי
        public DateTime Start { get; set; }
        //שעת יציאה בפועל
        public DateTime ActualStart { get; set; }
        //מספר תחנה אחרונה בקו שהאוטובוס עבר
        public int LastStationNumberPassedThrough { get; set; }
        //זמן מעבר בתחנה האחרונה הנ"ל
        public DateTime LastStationTimePassedThrough { get; set; }
        //זמן הגעה לתחנה הבאה
        public DateTime NextStationTimePassedThrough { get; set; }

        public string DriverID { get; set; }

    }
}
