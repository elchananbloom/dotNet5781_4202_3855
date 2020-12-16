using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_DALAPI;
using DO;
using I_DataSource;


namespace H_DataAccessLayer
{
    public sealed class DalObject : IDal
    {

        #region singleton implementaion
        private readonly static IDal mydal = new DalObject();
        private DalObject()
        {
            try
            {
                I_DataSource.DataSource.init();
            }
            catch (BusException be)
            {
                //TODO
            }
        }
        static DalObject() { }
        public static IDal Instance { get => mydal; }
        #endregion singleton


        #region IDal implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool addBus(BusDAO bus)
        {
            if (DataSource.Buses.Exists(mishehu => mishehu.LicenseNumber == bus.LicenseNumber))
            {
                throw new BusException("license exists allready");
                //return false;
            }
            DataSource.Buses.Add(bus.Clone1());
            return true;
        }

        public bool update(BusDAO bus)
        {
            if (!DataSource.Buses.Exists(mishehu => mishehu.LicenseNumber == bus.LicenseNumber))
            {
                return false;
            }

            /**
            Bus realBus = DataSource.Buses.First(mishehu => mishehu.License == bus.License);
            realBus.StartOfWork = bus.StartOfWork;
            realBus.TotalKms = bus.TotalKms;
            **/
            //delete
            DataSource.Buses.RemoveAll(b => b.LicenseNumber == bus.LicenseNumber);
            //insert
            //DataSource.Buses.Add(new BusDAO
            //{
            //    License = bus.License,
            //    StartOfWork = bus.StartOfWork,
            //    TotalKms = bus.TotalKms
            //});
            DataSource.Buses.Add(bus.Clone1());
            return true;

        }

        public List<BusDAO> getBusses()
        {
            List<BusDAO> result = new List<BusDAO>();
            foreach (var bus in I_DataSource.DataSource.Buses)
            {
                result.Add(bus.Clone1());
            }
            return result;
        }

        public BusDAO read(string license)
        {
            BusDAO result = default(BusDAO);
            result = I_DataSource.DataSource.Buses.FirstOrDefault(item => item.LicenseNumber == license);
            if (result != null)
            {
                //return new BusDAO     //clone (!) clown
                //{
                //    License = result.License,
                //    StartOfWork = result.StartOfWork,
                //    TotalKms = result.TotalKms
                //};
                return result.Clone1();
            }
            return null;
        }

        public bool addBusInTravel(BusInTravelDAO bus)
        {
            if (DataSource.BusesTravel.Exists(mishehu =>
                mishehu.LicenseNumber == bus.LicenseNumber
                && mishehu.Line == bus.Line
                && mishehu.Start == bus.Start))
            {
                // throw new InvalidOperationException("license exists allready")
                return false;
            }

            DataSource.BusesTravel.Add(bus.Clone2());
            return true;
        }

        public List<BusInTravelDAO> getBusesTravel()
        {
            List<BusInTravelDAO> travels = new List<BusInTravelDAO>();
            foreach (var busInTravel in I_DataSource.DataSource.BusesTravel)
            {
                travels.Add(busInTravel.Clone2());
            }
            return travels;
        }

        public void delete(BusDAO bus)
        {
            if (!I_DataSource.DataSource.Buses.Exists(item => item.LicenseNumber == bus.LicenseNumber))
            {
                //return false;
                throw new BusException("lo kayam bammarechet");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}

            I_DataSource.DataSource.Buses.RemoveAll(item => item.LicenseNumber == bus.LicenseNumber);
        }
        #endregion
    }


}

