using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusInTravelDAO
    {
        //we need to do real time information and the serial number//
        //מזהה אוטובוס בנסיעה- נותן ערך מספרי כמו תעודת זהות
        public int CurrentSerialNB { get; set; }
        //compound key
        public string LicenseNumber { get; set; }
        //מזהה קו שבביצוע
        public int LineNumber { get; set; }
        //שעת יציאה לקו הפורמלי
        public TimeSpan Start { get; set; }
        //שעת יציאה בפועל
        public TimeSpan ActualStart { get; set; }
        //מספר תחנה אחרונה בקו שהאוטובוס עבר
        public int LastStationNumberPassedThrough { get; set; }
        //זמן מעבר בתחנה האחרונה הנ"ל
        public TimeSpan LastStationTimePassedThrough { get; set; }
        //זמן הגעה לתחנה הבאה
        public TimeSpan NextStationTimePassedThrough { get; set; }
        //האם האוטובוס באמצע נסיעה או לא
        public bool IsActive { get; set; }
        public string DriverID { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
