using DO;
using G_DALAPI;
using I_DataSource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace H_DataAccessLayer
{
    public sealed class DalObject : IDal, IEnumerable
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


        #region BusDAO functions
        /// <summary>
        /// this func adds a new bus to the buses list.
        /// </summary>
        /// <param name="bus">Bus from DO</param>
        /// <returns></returns>
        public bool AddBus(BusDAO bus)
        {

            if (DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            {
                throw new BusException("License exists allready.");
                //return false;
            }
            BusDAO cloned = bus.Cloned();

            cloned.Deleted = false;
            DataSource.BussesList.Add(cloned);
            return true;
        }
        /// <summary>
        /// this func chooses a bus for driving.
        /// </summary>
        /// <param name="bus">Bus from DO</param>
        /// <returns></returns>
        public bool ChooseBusForDrive(BusDAO bus)
        {
            if (bus.Status == Status.READY_TO_DRIVE)
                return true;
            throw new BusException("The bus is not ready to drive.");
        }
        /// <summary>
        /// this func is for refuel bus.
        /// </summary>
        /// <param name="bus">Bus from DO</param>
        /// <returns></returns>
        public bool RefuelBus(BusDAO bus)
        {
            if ((bus.Status == Status.READY_TO_DRIVE || bus.Status == Status.NEED_REFUELING) && bus.Fuel != 1200)
            {
                return true;
            }
            throw new BusException("The bus needs to be refueled.");
        }
        /// <summary>
        /// this func is for trearment for the bus.
        /// </summary>
        /// <param name="bus">Bus from DO</param>
        /// <returns></returns>
        public bool Treatment(BusDAO bus)
        {
            if ((bus.Status == Status.READY_TO_DRIVE || bus.Status == Status.NEED_REFUELING || bus.Status == Status.NEED_TREATMENT) && bus.Maintnance != 0)
                return true;
            throw new BusException("The bus needs to be treated.");
        }
        /// <summary>
        /// Delete a bus by updating his delete field.
        /// </summary>
        /// <param name="bus">Bus from DO</param>
        /// <returns></returns>
        public bool RemoveBus(BusDAO bus)
        {
            foreach (var currentBus in I_DataSource.DataSource.BussesList)
            {
                if (currentBus.LicenseNumber == bus.LicenseNumber)
                {
                    currentBus.Deleted = true;
                    return true;
                }
            }
            throw new BusException("The bus does not exists in the system.");
            //if (!I_DataSource.DataSource.Buses.Exists(item => item.LicenseNumber == bus.LicenseNumber))
            //{
            //    throw new BusException("The bus does not exists in the system.");
            //}
            //BusDAO todelete = null;

            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}

            //I_DataSource.DataSource.Buses.RemoveAll(item => item.LicenseNumber == bus.LicenseNumber);
        }
        /// <summary>
        /// this func gets a bus by his license number.
        /// </summary>
        /// <param name="license">License number</param>
        /// <returns>Clone of the bus</returns>
        public BusDAO GetOneBus(string license)
        {
            BusDAO result = default(BusDAO);
            result = I_DataSource.DataSource.BussesList.FirstOrDefault(currentBus => currentBus.LicenseNumber == license);
            if (result != null)
            {
                return result.Cloned();
            }
            return null;
        }
        /// <summary>
        /// gets all busses in the list.
        /// </summary>
        /// <returns>List of busses</returns>
        public IEnumerable<BusDAO> GetAllBusses()
        {
            //result = new List<BusDAO>();
            //foreach (var bus in I_DataSource.DataSource.BussesList)
            //{
            //    if(bus.Deleted==false)
            //        result.Add(bus.Cloned());
            //}
            IEnumerable<BusDAO> result = from currentBus in I_DataSource.DataSource.BussesList
                                         where currentBus.Deleted == false
                                         select currentBus.Cloned();
            return result;
        }
        /// <summary>
        /// make the bus to be activated
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        public bool ActivateBus(string license)
        {
            foreach (var currentBus in DataSource.BussesList)
            {
                if (currentBus.LicenseNumber == license)
                {
                    currentBus.Deleted = true;
                    return true;
                }
            }
            //if (DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == license))
            //{
            //    throw new BusException("The bus already activated.");
            //}
            throw new BusException("The bus does not exists in the system.");
        }
        public bool UpdateBus(BusDAO bus)
        {
            if (!DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            {
                return false;
            }
            //delete
            DataSource.BussesList.RemoveAll(currentBus => currentBus.LicenseNumber == bus.LicenseNumber);
            //insert
            DataSource.BussesList.Add(bus.Cloned());
            return true;

        }
        #endregion


        #region BusInTravelDAO functions

        /// <summary>
        /// add bus in travel to the bussesInTravel list
        /// </summary>
        /// <param name="bus">Bus from BusInTravelDAO</param>
        /// <returns></returns>
        public bool AddBusInTravel(BusInTravelDAO bus)
        {
            if (DataSource.BussesInTravelList.Exists(currentBusInTravel =>
                currentBusInTravel.LicenseNumber == bus.LicenseNumber
                && currentBusInTravel.LineNumber == bus.LineNumber
                && currentBusInTravel.Start == bus.Start
                && bus.IsActive))
            {
                // throw new InvalidOperationException("license exists allready")
                //return false;
                throw new BusException("The bus is already in travel.");
            }
            BusInTravelDAO cloned = bus.Cloned();

            cloned.CurrentSerialNB = Configuration.SerialBusInTravel;
            cloned.IsActive = true;
            DataSource.BussesInTravelList.Add(cloned);

            return true;
        }
        /// <summary>
        /// remove bus in travel to the bussesInTravel list
        /// </summary>
        /// <param name="bus">Bus from BusInTravelDAO</param>
        /// <returns></returns>
        public bool RemoveBusInTravel(BusInTravelDAO bus)
        {
            foreach (var item in I_DataSource.DataSource.BussesInTravelList)
            {
                if (DataSource.BussesInTravelList.Exists(currentBusInTravel =>
                currentBusInTravel.LicenseNumber == bus.LicenseNumber
                && currentBusInTravel.LineNumber == bus.LineNumber
                && currentBusInTravel.Start == bus.Start
                && bus.IsActive))
                {
                    item.IsActive = false;
                    //I_DataSource.DataSource.BussesInTravel.Remove(item);
                    return true;
                }
            }
            throw new BusException("The bus is not traveling.");
        }
        /// <summary>
        /// gets all busses in the bussesInTravel list.
        /// </summary>
        /// <returns></returns>
        public List<BusInTravelDAO> GetAllBusesInTravel()
        {
            List<BusInTravelDAO> travels = new List<BusInTravelDAO>();
            foreach (var busInTravel in I_DataSource.DataSource.BussesInTravelList)
            {
                travels.Add(busInTravel.Cloned());
            }
            return travels;
        }
        public bool UpdateBusInTravel(BusInTravelDAO busInTravel)
        {
            if (!DataSource.BussesInTravelList.Exists(currentBusInTravel => currentBusInTravel.LicenseNumber == busInTravel.LicenseNumber))
            {
                return false;
            }
            //delete
            DataSource.BussesInTravelList.RemoveAll(currentBusInTravel => currentBusInTravel.LicenseNumber == busInTravel.LicenseNumber);
            //insert
            DataSource.BussesInTravelList.Add(busInTravel.Cloned());
            return true;
        }
        /// <summary>
        /// this funp overrides the original CompareTo function.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj1"></param>
        /// <returns></returns>
        public int CompareTo(Object obj, Object obj1)
        {
            int stationLine1 = ((StationLineDAO)obj).NumberStationInLine;
            int stationLine2 = ((StationLineDAO)obj1).NumberStationInLine;
            return stationLine1.CompareTo(stationLine2);
        }
        /// <summary>
        /// implement IEnumerator 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return I_DataSource.DataSource.StationLinesList.GetEnumerator();
        }
        #endregion


        #region StationLineDAO functions
        /// <summary>
        /// add station line to the station lines list.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        public bool AddStationLine(StationLineDAO stationLine)
        {
            if (I_DataSource.DataSource.StationLinesList.Exists(currentStationLine => currentStationLine.LineNumber == stationLine.LineNumber
             && currentStationLine.StationNumber == stationLine.StationNumber))
            {
                throw new BusException("The station line already exists in the system.");
            }
            StationLineDAO cloned = stationLine.Cloned();
            cloned.Deleted = false;
            I_DataSource.DataSource.StationLinesList.Insert(stationLine.NumberStationInLine, cloned);

            int loc = stationLine.NumberStationInLine;
            for (int i = stationLine.NumberStationInLine; i < I_DataSource.DataSource.StationLinesList.Count; i++)
            {
                I_DataSource.DataSource.StationLinesList[i].Cloned().NumberStationInLine = loc++;
            }
            return true;
        }
        /// <summary>
        /// Delete a station line by updating his delete field.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        public bool RemoveStationLine(StationLineDAO stationLine)
        {
            foreach (var currentStationLine in I_DataSource.DataSource.StationLinesList)
            {
                if (currentStationLine.LineNumber == stationLine.LineNumber
                    && currentStationLine.StationNumber == stationLine.StationNumber)
                {
                    for (int i = I_DataSource.DataSource.StationLinesList.Count; i > stationLine.NumberStationInLine; i--)
                    {
                        I_DataSource.DataSource.StationLinesList[i].Cloned().NumberStationInLine = i - 1;
                    }
                    currentStationLine.Deleted = true;
                    return true;
                }
            }
            throw new BusException("The station line does not exists in the system.");
        }
        /// <summary>
        /// make the station line to be activated
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        public bool ActivateStationLine(int lineNumber, int stationNumber)
        {
            foreach (var currentStationLine in DataSource.StationLinesList)
            {
                if (currentStationLine.LineNumber == lineNumber
                    && currentStationLine.StationNumber == stationNumber)
                {
                    currentStationLine.Deleted = true;
                    return true;
                }
            }
            throw new BusException("The station line does not exists in the system.");
        }
        /// <summary>
        /// this func gets a station line by his line number and his station number.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        public StationLineDAO GetOneStationLine(int lineNumber, int stationNumber)
        {
            StationLineDAO result = default(StationLineDAO);
            result = I_DataSource.DataSource.StationLinesList.FirstOrDefault(item => item.LineNumber == lineNumber
            && item.StationNumber == stationNumber);
            if (result != null)
            {
                return result.Cloned();
            }
            return null;
        }
        /// <summary>
        /// gets all the station lines inside the bus line
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns>all the stations in the bus line</returns>
        public List<StationLineDAO> GetAllStationsLineOfBusLine(int lineNumber)
        {
            IEnumerable<StationLineDAO> result = from currentStationLine in I_DataSource.DataSource.StationLinesList
                                                 where currentStationLine.LineNumber == lineNumber && !currentStationLine.Deleted
                                                 select currentStationLine.Cloned();
            //IEnumerable<StationLineDAO> result = new List<StationLineDAO>();
            //foreach (var s in I_DataSource.DataSource.Stations)
            //{
            //    if (s.LineNumber == lLineNumber)
            //        result.Add(s.Cloned());

            //}
            List<StationLineDAO> stationLines = result as List<StationLineDAO>;
            stationLines.Sort();
            return stationLines;
        }
        public bool UpdateStationLine(StationLineDAO stationLine)
        {
            if (!DataSource.StationLinesList.Exists(currentStationLine => currentStationLine.StationNumber == stationLine.StationNumber
            && currentStationLine.LineNumber == stationLine.LineNumber))
            {
                return false;
            }
            //delete
            DataSource.StationLinesList.RemoveAll(currentStationLine => currentStationLine.StationNumber == stationLine.StationNumber
            && currentStationLine.LineNumber == stationLine.LineNumber);
            //insert
            DataSource.StationLinesList.Add(stationLine.Cloned());
            return true;
        }
        #endregion


        #region StationDAO functions
        /// <summary>
        /// this func adds a new station to the stations list.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool AddStation(StationDAO station)
        {
            if (DataSource.StationsList.Exists(currentStation => currentStation.StationNumber == station.StationNumber))
            {
                throw new BusException("Station already exists.");
                //return false;
            }
            StationDAO cloned = station.Cloned();

            cloned.Deleted = false;
            DataSource.StationsList.Add(cloned);
            return true;
        }
        /// <summary>
        /// this func remove a station to the stations list.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool RemoveStation(StationDAO station)
        {
            foreach (var currentStation in I_DataSource.DataSource.StationsList)
            {
                if (currentStation.StationNumber == station.StationNumber)
                {
                    currentStation.Deleted = true;
                    return true;
                }
            }
            throw new BusException("The station does not exists in the system.");
        }
        /// <summary>
        /// this func gets one station from the stations list
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        public StationDAO GetOneStation(int stationNumber)
        {
            StationDAO result = default(StationDAO);
            result = I_DataSource.DataSource.StationsList.FirstOrDefault(item => item.StationNumber == stationNumber);
            if (result != null)
            {
                return result.Cloned();
            }
            return null;
        }
        /// <summary>
        /// gets all stations from the stations list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationDAO> GetAllStations()
        {
            IEnumerable<StationDAO> result = from currentStation in I_DataSource.DataSource.StationsList
                                             where currentStation.Deleted == false
                                             select currentStation.Cloned();
            return result;
        }
        /// <summary>
        /// gets all the bus lines in the station
        /// </summary>
        /// <param name="stationNmber"></param>
        /// <returns></returns>
        public List<BusLineDAO> GetAllBusLinesInStation(int stationNmber)
        {
            IEnumerable<BusLineDAO> result = from currentBusLine in DataSource.StationLinesList
                                             where currentBusLine.StationNumber == stationNmber && !currentBusLine.Deleted
                                             select GetOneBusLine(currentBusLine.LineNumber);
            List<BusLineDAO> busLinesInStation = result as List<BusLineDAO>;
            if (busLinesInStation.Count == 0)
            {
                throw new BusException("The station does not exists.");
            }
            return busLinesInStation;
        }
        public bool UpdateStation(StationDAO station)
        {
            if (!DataSource.StationsList.Exists(currentStation => currentStation.StationNumber == station.StationNumber))
            {
                return false;
            }
            //delete
            DataSource.StationsList.RemoveAll(currentStation => currentStation.StationNumber == station.StationNumber);
            //insert
            DataSource.StationsList.Add(station.Cloned());
            return true;
        }

        //public int CompareTo(StationLineDAO stationLine1, StationLineDAO stationLine2)
        //{
        //    return this.CompareTo(stationLine1, stationLine2);
        //}




        #endregion


        #region BusLineDAO functions
        /// <summary>
        /// adds a new bus line to the bus lines list.
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        public bool AddBusLine(BusLineDAO busLine)
        {
            if (DataSource.BusLinesList.Exists(currentBus => currentBus.LineNumber == busLine.LineNumber))
            {
                throw new BusException("The bus line already exists.");
                //return false;
            }
            BusLineDAO cloned = busLine.Cloned();
            cloned.CurrentSerialNB = Configuration.SerialBusLine;
            cloned.Deleted = false;
            DataSource.BusLinesList.Add(cloned);

            StationLineDAO firstStation = GetOneStationLine(busLine.LineNumber, busLine.FirstStationNumber).Cloned();
            firstStation.NumberStationInLine = 0;
            StationLineDAO lastStation = GetOneStationLine(busLine.LineNumber, busLine.LastStationNumber).Cloned();
            lastStation.NumberStationInLine = 1;
            DataSource.StationLinesList.Add(firstStation);
            DataSource.StationLinesList.Add(lastStation);
            return true;
        }
        /// <summary>
        /// remove a bus line from the bus lines list.
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        public bool RemoveBusLine(BusLineDAO busLine)
        {
            foreach (var currentBusLine in I_DataSource.DataSource.BusLinesList)
            {
                if (currentBusLine.LineNumber == busLine.LineNumber)
                {
                    currentBusLine.Deleted = true;
                    foreach (StationLineDAO currentStationLine in DataSource.StationLinesList)
                    {
                        if(currentStationLine.LineNumber==currentBusLine.LineNumber)
                        {
                            currentStationLine.Deleted = true;
                        }
                    }
                    return true;
                }
            }
            throw new BusException("The bus line does not exists in the system.");
        }
        /// <summary>
        /// gets bus line from the bus line list.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public BusLineDAO GetOneBusLine(int lineNumber)
        {
            BusLineDAO result = default(BusLineDAO);
            result = I_DataSource.DataSource.BusLinesList.FirstOrDefault(currentBusLine => currentBusLine.LineNumber == lineNumber);
            if (result != null)
            {
                return result.Cloned();
            }
            return null;
        }
        /// <summary>
        ///  gets all bus lines from the bus line list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusLineDAO> GetAllBusLines()
        {
            IEnumerable<BusLineDAO> result = from currentBusLine in I_DataSource.DataSource.BusLinesList
                                             where currentBusLine.Deleted == false
                                             select currentBusLine.Cloned();
            return result;
        }
        public bool UpdateBusLine(BusLineDAO busLine)
        {
            if (!DataSource.BusLinesList.Exists(currentBusLine => currentBusLine.LineNumber == busLine.LineNumber))
            {
                return false;
            }
            //delete
            DataSource.BusLinesList.RemoveAll(currentBusLine => currentBusLine.LineNumber == busLine.LineNumber);
            //insert
            DataSource.BusLinesList.Add(busLine.Cloned());
            return true;
        }
        #endregion


        public List<CoupleStationInRowDAO> GetAllCoupleStationInRow()
        {
            Random rand = new Random();
            foreach (BusLineDAO busLine in GetAllBusLines())
            {
                List<StationLineDAO> stationLines = GetAllStationsLineOfBusLine(busLine.LineNumber);
                for (int i = 0; i < stationLines.Count - 1; i++)
                {
                    CoupleStationInRowDAO stationInRow = new CoupleStationInRowDAO
                    {
                        StationNumberOne = stationLines[i].StationNumber,
                        StationNumberTwo = stationLines[i + 1].StationNumber,
                        AverageTravelTime = new TimeSpan(rand.Next(0), rand.Next(10), rand.Next(59)),
                        Distance = rand.Next(2000)
                    };
                    if (!DataSource.CoupleStationInRowList.Exists(s => s.StationNumberOne == stationInRow.StationNumberOne
                     && s.StationNumberTwo == stationInRow.StationNumberTwo))
                    {
                        DataSource.CoupleStationInRowList.Add(stationInRow.Cloned());
                    }
                }
            }
            return DataSource.CoupleStationInRowList;
        }
        public bool AddCoupleStationInRow(int stationNumberOne, int stationNumberTwo, TimeSpan time, int distance)
        {
            if (DataSource.CoupleStationInRowList.Exists(currentCoupleStation => currentCoupleStation.StationNumberOne == stationNumberOne
             && currentCoupleStation.StationNumberTwo == stationNumberTwo))
            {
                throw new BusException("This couple stations already exists in the system.");
            }
            CoupleStationInRowDAO coupleStation = new CoupleStationInRowDAO
            {
                StationNumberOne = stationNumberTwo,
                StationNumberTwo = stationNumberTwo,
                AverageTravelTime = time,
                Distance = distance
            };
            DataSource.CoupleStationInRowList.Add(coupleStation.Cloned());
            return true;
        }
        #endregion
    }


}

