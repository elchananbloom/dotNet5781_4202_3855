using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_4202_3855
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
        public Bus() { }

        public Bus(string licenseNumber, int maintenance, DateTime lastTreatment, int totalMileage, int fuelStatus, DateTime dateBegin)
        {
            this.licenseNumber = licenseNumber;
            this.maintenance = maintenance;
            this.lastTreatment = lastTreatment;
            this.totalMileage = totalMileage;
            this.fuelStatus = fuelStatus;
            this.dateBegin = dateBegin;
        }
        public override string ToString()
        {
            String result = "";
            result+= "\nLicense number: "+LicenseNumber+"\nCommencement of activity: " + DateBegin + "\nTotal mileage: " + TotalMileage + "\nLast maintenance day: " + LastTreatment
                + "\nKilometer driven since last maintenance: " + Maintenance + "\nKilometers left from last fueling: " + FuelStatus; 
            return result;
        }
    }

   
}

