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
                if ((DateBegin.Year < 2018 && value.Length == 7) || (DateBegin.Year >= 2018 && value.Length == 8))
                {
                    if (value.Length == 7)
                    {
                        string begin = value.Substring(0, 2);
                        string middle = value.Substring(2, 3);
                        string end = value.Substring(5, 2);
                        licenseNumber = String.Format("{0}-{1}-{2}", begin, middle, end);
                    }
                    else if(value.Length == 8)
                    {
                        string begin = value.Substring(0, 3);
                        string middle = value.Substring(3, 2);
                        string end = value.Substring(5, 3);
                        licenseNumber = String.Format("{0}-{1}-{2}", begin, middle, end);
                    }
                }
                else
                {
                    //result = false;
                    throw new Exception("Error. Incorrect license number.\nEnter a license number again.");
                }
            }
        }
        public int Maintenance
        {
            get { return maintenance; }
            set 
            { 
                Random rand = new Random(); 
                maintenance = rand.Next(20000);
            }
        }
        public int TotalMileage
        {
            get { return totalMileage; }
            set 
            {
                Random rand = new Random();
                totalMileage = rand.Next(500000); 
            }
        }
        public int FuelStatus
        {
            get { return fuelStatus; }
            set { fuelStatus = value; }
        }
        public Bus(string licenceNumber, DateTime Date, int fuel)//this function is for adding a new bus to the list by getting it's licence number and it's date commencement of activity.
        {
            this.DateBegin = Date;
            this.FuelStatus = fuel;
            this.LicenseNumber = licenceNumber;
        }
        public Bus() { }

        public Bus(string licenseNumber, int maintenance, DateTime lastTreatment, int totalMileage, int fuelStatus, DateTime dateBegin)
        {
            this.DateBegin = dateBegin;
            this.LicenseNumber = licenseNumber;
            this.Maintenance = maintenance;
            this.LastTreatment = lastTreatment;
            this.TotalMileage = totalMileage;
            this.FuelStatus = fuelStatus;
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

