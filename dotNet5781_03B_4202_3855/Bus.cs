﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace dotNet5781_03B_4202_3855
{
    public class Bus
    {
        private int status;
        private string licenseNumber;
        private int maintenance;
        private DateTime lastTreatment;
        private int totalMileage;
        private int fuelStatus;
        private DateTime dateBegin;
        
        /// <summary>
        /// properties.
        /// </summary>
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
                    throw new Exception("Error. Incorrect license number.\nEnter a license number again.");
                }
            }
        }
        public int Maintenance
        {
            get { return maintenance; }
            set 
            {
                maintenance = value;
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
        public int Status
        {
            get
            {
                return status;
            }
            set 
            {
                if (FuelStatus <= 0)
                    value= (int)BusStatus.NEED_REFUELING;
                if (maintenance >= 20000 || LastTreatment <= DateTime.Now.AddYears(-1))
                    value= (int)BusStatus.NEED_TREATMENT;
                status =value;
            }
        }

        /// <summary>
        ///         //this function is for adding a new bus to the list by getting it's licence number and it's date commencement of activity.
        /// </summary>
        /// <param name="licenceNumber"></param>
        /// <param name="Date"></param>
        /// <param name="fuel"></param>
        public Bus(string licenceNumber, DateTime Date, int fuel)
        {
            this.DateBegin = Date;
            this.FuelStatus = fuel;
            this.LicenseNumber = licenceNumber;
            Status = status;
        }
        /// <summary>
       /// ctor.
       /// </summary>
        public Bus() { }
        /// <summary>
        /// ctor with parameters.
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="maintenance"></param>
        /// <param name="lastTreatment"></param>
        /// <param name="totalMileage"></param>
        /// <param name="fuelStatus"></param>
        /// <param name="dateBegin"></param>
        public Bus(string licenseNumber, int maintenance, DateTime lastTreatment, int totalMileage, int fuelStatus, DateTime dateBegin)
        {
            this.DateBegin = dateBegin;
            this.LicenseNumber = licenseNumber;
            this.maintenance = maintenance;
            this.LastTreatment = lastTreatment;
            this.TotalMileage = totalMileage;
            this.FuelStatus = fuelStatus;
            Status = status;
        }

        /// <summary>
        /// a func for the printing.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String result = "";
            result+= "\nLicense number: "+licenseNumber+"\nCommencement of activity: " + dateBegin + "\nTotal mileage: " + totalMileage + "\nLast maintenance day: " + lastTreatment
                + "\nKilometer driven since last maintenance: " + maintenance + "\nKilometers left from last fueling: " + fuelStatus; 
            return result;
        }
    }
}

