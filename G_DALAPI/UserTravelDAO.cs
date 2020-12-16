using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserTravelDAO
    {
        //מזהה נסיעת משתמש
        private static int userTravelSerial = 1;
        public string UserName { get; set; }
        public int LineNumber { get; set; }
        //מזהה תחנת עליה
        public int BourdingStationNumber { get; set; }
        //זמן עליה
        public DateTime BourdingStationTime { get; set; }
        //מזהה תחנת ירידה
        public int GetOffStationNumber { get; set; }
        //זמן ירידה
        public DateTime GetOffStationTime { get; set; }

    }
}
