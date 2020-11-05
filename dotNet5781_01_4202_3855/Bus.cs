using System;

namespace dotNet_01_4202_3855
{
    public class Bus
    {
        private string licenseNumber;//the set function will call another function that checks the correctness of a licence number.
        private int maintenance;
        private DateTime lastTreatment;
        private int totalMileage;
        private int fuelStatus;
        private DateTime dateBegin;

        public DateTime DateBegin
        {
            get { return dateBegin; }
            set { dateBegin = value; }
        }
        public DateTime LastTreatment
        {
            get { return lastTreatment; }
            set { lastTreatment = value; }
        }
        public string LicenseNumber
        {
            get
            {
                return licenseNumber;
            }
            set
            {
                this.licenseNumber = value;
            }
        }
        public int Maintenance
        {
            get { return maintenance; }
            set { maintenance = value; }
        }
        public int TotalMileage
        {
            get { return totalMileage; }
            set { totalMileage = value; }
        }
        public int FuelStatus
        {
            get { return fuelStatus; }
            set { fuelStatus = value; }
        }
        public Bus(string licenceNumber, DateTime Date, int fuel)//this function is for adding a new bus to the list by getting it's licence number and it's date commencement of activity.
        {
            this.fuelStatus = fuel;
            this.licenseNumber = licenceNumber;
            this.dateBegin = Date;
        }
    }
}
