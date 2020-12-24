using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusInTravelBO
    {
        //we need to do real time information and the serial number//
        //מזהה אוטובוס בנסיעה- נותן ערך מספרי כמו תעודת זהות
        //public int CurrentSerialNB { get; set; }
        //compound key
        public BusBO Bus { get; set; }
        //מזהה קו שבביצוע
        public BusLineBO BusLine { get; set; }
        //שעת יציאה לקו הפורמלי
        public DateTime Start { get; set; }
        //שעת יציאה בפועל
        public DateTime ActualStart { get; set; }
        // תחנה אחרונה בקו שהאוטובוס עבר
        public StationLineBO LastStationPassedThrough { get; set; }
        public List<CoupleStationInRowBO> CoupleStationInRow { get; set; }
        //זמן מעבר בתחנה האחרונה הנ"ל
        public DateTime LastStationTimePassedThrough { get; set; }
        //זמן הגעה לתחנה הבאה
        public DateTime NextStationTimePassedThrough { get; set; }
        //האם האוטובוס באמצע נסיעה או לא
        public bool IsActive { get; set; }
        public string DriverID { get; set; }
    }
}
