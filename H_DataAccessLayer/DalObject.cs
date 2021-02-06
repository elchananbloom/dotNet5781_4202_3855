using DO;
using DALAPI;
using DataSource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    sealed class DalObject : IDal
    {
        #region singleton implementaion
        private readonly static IDal mydal = new DalObject();
        private DalObject()
        {
            try
            {
                DataSource.DataSource.init();
            }
            catch (BusException be)
            {
                //throw new BusException(be.Message);
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

            if (DataSource.DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            {
                throw new BusException("License exists allready.");
                //return false;
            }
            //BusDAO cloned = ;
            //cloned.Deleted = false;
            DataSource.DataSource.BussesList.Add(bus.Cloned());
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
            foreach (var currentBus in DataSource.DataSource.BussesList)
            {
                if (currentBus.LicenseNumber == bus.LicenseNumber)
                {
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
            result = DataSource.DataSource.BussesList.FirstOrDefault(currentBus => currentBus.LicenseNumber == license);
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
            IEnumerable<BusDAO> result = from currentBus in DataSource.DataSource.BussesList
                                         //where urrentBus.Deleted == false
                                         select currentBus.Cloned();
            return result;
        }
        /// <summary>
        /// make the bus to be activated
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        /// 

        //public bool ActivateBus(string license)
        //{
        //    foreach (var currentBus in DataSource.BussesList)
        //    {
        //        if (currentBus.LicenseNumber == license)
        //        {
        //            currentBus.Deleted = true;
        //            return true;
        //        }
        //    }
        //    //if (DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == license))
        //    //{
        //    //    throw new BusException("The bus already activated.");
        //    //}
        //    throw new BusException("The bus does not exists in the system.");
        //}
        /// <summary>
        /// bus update
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool UpdateBus(BusDAO bus)
        {
            if (!DataSource.DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            {
                throw new BusException("The bus does not exist in the system.");
            }
            //delete
            DataSource.DataSource.BussesList.RemoveAll(currentBus => currentBus.LicenseNumber == bus.LicenseNumber);
            //insert
            DataSource.DataSource.BussesList.Add(bus.Cloned());
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
            if (DataSource.DataSource.BussesInTravelList.Exists(currentBusInTravel =>
                currentBusInTravel.LicenseNumber == bus.LicenseNumber
                && currentBusInTravel.CurrentSerialNB == bus.CurrentSerialNB
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
            DataSource.DataSource.BussesInTravelList.Add(cloned);
            return true;
        }
        /// <summary>
        /// remove bus in travel to the bussesInTravel list
        /// </summary>
        /// <param name="bus">Bus from BusInTravelDAO</param>
        /// <returns></returns>
        public bool RemoveBusInTravel(BusInTravelDAO bus)
        {
            foreach (var item in DataSource.DataSource.BussesInTravelList)
            {
                if (DataSource.DataSource.BussesInTravelList.Exists(currentBusInTravel =>
                currentBusInTravel.LicenseNumber == bus.LicenseNumber
                && currentBusInTravel.CurrentSerialNB == bus.CurrentSerialNB
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
            foreach (var busInTravel in DataSource.DataSource.BussesInTravelList)
            {
                travels.Add(busInTravel.Cloned());
            }
            return travels;
        }
        /// <summary>
        /// bus in travel update
        /// </summary>
        /// <param name="busInTravel"></param>
        /// <returns></returns>
        public bool UpdateBusInTravel(BusInTravelDAO busInTravel)
        {
            if (!DataSource.DataSource.BussesInTravelList.Exists(currentBusInTravel => currentBusInTravel.LicenseNumber == busInTravel.LicenseNumber))
            {
                return false;
            }
            //delete
            DataSource.DataSource.BussesInTravelList.RemoveAll(currentBusInTravel => currentBusInTravel.LicenseNumber == busInTravel.LicenseNumber);
            //insert
            DataSource.DataSource.BussesInTravelList.Add(busInTravel.Cloned());
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
            return DataSource.DataSource.StationLinesList.GetEnumerator();
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
            if (DataSource.DataSource.StationLinesList.Exists(currentStationLine => currentStationLine.LineNumber == stationLine.LineNumber
             && currentStationLine.StationNumber == stationLine.StationNumber
             &&!currentStationLine.Deleted))
            {
                throw new BusException("The station line already exists in the system.");
            }
            //StationLineDAO cloned = stationLine.Cloned();
            //cloned.Deleted = false;
            IEnumerable<StationLineDAO> list = new List<StationLineDAO>(GetAllStationsLineOfBusLine(stationLine.LineNumber));
            for (int i = stationLine.NumberStationInLine; i < GetAllStationsLineOfBusLine(stationLine.LineNumber).Count(); i++)
            {
                list.ToList()[i].NumberStationInLine = i + 1;
                //DataSource.DataSource.StationLinesList[i].NumberStationInLine = i - 1;
            }
            //CoupleStationInRowDAO coupleStation1 = GetOneCoupleStationInRow(list.ToList()[stationLine.NumberStationInLine - 1].StationNumber, stationLine.StationNumber);
            //CoupleStationInRowDAO coupleStation2 = GetOneCoupleStationInRow(stationLine.StationNumber, list.ToList()[stationLine.NumberStationInLine].StationNumber);
            //CoupleStationInRowDAO coupleStation = new CoupleStationInRowDAO
            //{
            //    StationNumberOne = coupleStation1.StationNumberOne,
            //    StationNumberTwo = list.ToList()[stationLine.NumberStationInLine].StationNumber,
            //    Distance = coupleStation1.Distance + coupleStation2.Distance,
            //    AverageTravelTime = coupleStation1.AverageTravelTime + coupleStation2.AverageTravelTime
            //};
            //if (!DataSource.DataSource.CoupleStationInRowList.Exists(s => s == coupleStation))
            //{
            //    DataSource.DataSource.CoupleStationInRowList.Add(coupleStation);
            //}
            foreach (var item in list)
            {
                UpdateStationLine(item);
            }
            DataSource.DataSource.StationLinesList.Insert(stationLine.NumberStationInLine, stationLine.Cloned());
            return true;
            //int loc = stationLine.NumberStationInLine;
            //for (int i = stationLine.NumberStationInLine; i < DataSource.DataSource.StationLinesList.Count; i++)
            //{
            //    DataSource.DataSource.StationLinesList[i].Cloned().NumberStationInLine = loc++;
            //}
            //return true;
        }
        /// <summary>
        /// Delete a station line by updating his delete field.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        public bool RemoveStationLine(StationLineDAO stationLine)
        {
            foreach (var currentStationLine in DataSource.DataSource.StationLinesList)
            {
                if (currentStationLine.LineNumber == stationLine.LineNumber
                    && currentStationLine.StationNumber == stationLine.StationNumber)
                {
                    currentStationLine.Deleted = true;
                    IEnumerable<StationLineDAO> list = new List<StationLineDAO>(GetAllStationsLineOfBusLine(stationLine.LineNumber));
                    for (int i = GetAllStationsLineOfBusLine(stationLine.LineNumber).Count()-1; i >= stationLine.NumberStationInLine; i--)
                    {
                        list.ToList()[i].NumberStationInLine = i;
                        //DataSource.DataSource.StationLinesList[i].NumberStationInLine = i - 1;
                    }

                    if (stationLine.NumberStationInLine > 0 && stationLine.NumberStationInLine < list.Count())
                    {
                        CoupleStationInRowDAO coupleStation1 = GetOneCoupleStationInRow(list.ToList()[stationLine.NumberStationInLine - 1].StationNumber, stationLine.StationNumber);
                        CoupleStationInRowDAO coupleStation2 = GetOneCoupleStationInRow(stationLine.StationNumber, list.ToList()[stationLine.NumberStationInLine].StationNumber);
                        CoupleStationInRowDAO coupleStation = new CoupleStationInRowDAO
                        {
                            StationNumberOne = coupleStation1.StationNumberOne,
                            StationNumberTwo = list.ToList()[stationLine.NumberStationInLine].StationNumber,
                            Distance = coupleStation1.Distance + coupleStation2.Distance,
                            AverageTravelTime = coupleStation1.AverageTravelTime + coupleStation2.AverageTravelTime
                        };
                        if (!DataSource.DataSource.CoupleStationInRowList.Exists(s => s == coupleStation))
                        {
                            DataSource.DataSource.CoupleStationInRowList.Add(coupleStation);
                        } 
                    }
                    
                    //DataSource.DataSource.CoupleStationInRowList.Add(new CoupleStationInRowDAO
                    //{
                    //    StationNumberOne = coupleStation1.StationNumberOne,
                    //    StationNumberTwo = list.ToList()[stationLine.NumberStationInLine].StationNumber,
                    //    Distance = coupleStation1.Distance + coupleStation2.Distance,
                    //    AverageTravelTime = coupleStation1.AverageTravelTime + coupleStation2.AverageTravelTime
                    //});
                    foreach (var item in list)
                    {
                        UpdateStationLine(item);
                    }
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

        //public bool ActivateStationLine(int lineNumber, int stationNumber)
        //{
        //    foreach (var currentStationLine in DataSource.StationLinesList)
        //    {
        //        if (currentStationLine.CurrentSerialNB == lineNumber
        //            && currentStationLine.StationNumber == stationNumber)
        //        {
        //            currentStationLine.Deleted = true;
        //            return true;
        //        }
        //    }
        //    throw new BusException("The station line does not exists in the system.");
        //}

        /// <summary>
        /// this func gets a station line by his line number and his station number.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        public StationLineDAO GetOneStationLine(int lineNumber, int stationNumber)
        {
            StationLineDAO result = default(StationLineDAO);
            result = DataSource.DataSource.StationLinesList.FirstOrDefault(item => item.LineNumber == lineNumber
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
        public IEnumerable<StationLineDAO> GetAllStationsLineOfBusLine(int lineNumber)
        {
            IEnumerable<StationLineDAO> result = from currentStationLine in DataSource.DataSource.StationLinesList
                                                where currentStationLine.LineNumber == lineNumber && !currentStationLine.Deleted
                                                 select currentStationLine.Cloned();
            //IEnumerable<StationLineDAO> result = new List<StationLineDAO>();
            //foreach (var s in I_DataSource.DataSource.Stations)
            //{
            //    if (s.CurrentSerialNB == lLineNumber)
            //        result.Add(s.Cloned());

            //}

            //List<StationLineDAO> stationLines = result as List<StationLineDAO>;
            //stationLines.Sort();
            return result;
        }
        /// <summary>
        /// station line update
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        public bool UpdateStationLine(StationLineDAO stationLine)
        {
            if (!DataSource.DataSource.StationLinesList.Exists(currentStationLine => currentStationLine.StationNumber == stationLine.StationNumber
            && currentStationLine.LineNumber == stationLine.LineNumber))
            {
                return false;
            }
            //delete
            DataSource.DataSource.StationLinesList.RemoveAll(currentStationLine => currentStationLine.StationNumber == stationLine.StationNumber
            && currentStationLine.LineNumber == stationLine.LineNumber);
            //insert
            //AddStationLine(stationLine);
            DataSource.DataSource.StationLinesList.Insert(stationLine.NumberStationInLine, stationLine.Cloned());
            return true;
        }
        public IEnumerable<StationLineDAO> GetAllStationLines()
        {
            return from item in DataSource.DataSource.StationLinesList
                   select item;
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
            if (DataSource.DataSource.StationsList.Exists(currentStation => currentStation.StationNumber == station.StationNumber))
            {
                throw new BusException("Station already exists.");
                //return false;
            }
            //StationDAO cloned = station.Cloned();
            //cloned.Deleted = false;
            DataSource.DataSource.StationsList.Add(station.Cloned());
            return true;
        }
        /// <summary>
        /// this func remove a station to the stations list.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool RemoveStation(StationDAO station)
        {
            for(int i=0;i< DataSource.DataSource.StationsList.Count;i++)
            {
                var currentStation = DataSource.DataSource.StationsList[i];
                if (currentStation.StationNumber == station.StationNumber)
                {
                    currentStation.Deleted = true;
                    for(int j=0;j< DataSource.DataSource.StationLinesList.Count;j++)
                    {
                        var item = DataSource.DataSource.StationLinesList[j];
                        if (item.StationNumber == station.StationNumber)
                        {
                            RemoveStationLine(item);
                        }
                    }
                    return true;
                }
            }
            //foreach (var currentStation in DataSource.DataSource.StationsList)
            //{
            //    if (currentStation.StationNumber == station.StationNumber)
            //    {
            //        currentStation.Deleted = true;
            //        foreach (var item in DataSource.DataSource.StationLinesList)
            //        {
            //            if(item.StationNumber==station.StationNumber)
            //            {
            //                RemoveStationLine(item);
            //            }
            //        }
            //        return true;
            //    }
            //}
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
            result = DataSource.DataSource.StationsList.FirstOrDefault(item => item.StationNumber == stationNumber);
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
            IEnumerable<StationDAO> result = from currentStation in DataSource.DataSource.StationsList
                                             //where currentStation.Deleted == false
                                             select currentStation.Cloned();
            return result;
        }
        /// <summary>
        /// gets all the bus lines in the station
        /// </summary>
        /// <param name="stationNmber"></param>
        /// <returns></returns>
        public IEnumerable<BusLineDAO> GetBusLineInStation(int stationNmber)
        {
            IEnumerable<BusLineDAO> result = from currentStationLine in DataSource.DataSource.StationLinesList
                                             where currentStationLine.StationNumber == stationNmber && !currentStationLine.Deleted
                                             select GetOneBusLine(currentStationLine.LineNumber);
            //List<BusLineDAO> busLinesInStation = result as List<BusLineDAO>;
            //if (busLinesInStation.Count == 0)
            //{
            //    throw new BusException("The station does not exists.");
            //}
            return result;
        }
        /// <summary>
        /// station update
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool UpdateStation(StationDAO station)
        {
            if (!DataSource.DataSource.StationsList.Exists(currentStation => currentStation.StationNumber == station.StationNumber))
            {
                return false;
            }
            //delete
            DataSource.DataSource.StationsList.RemoveAll(currentStation => currentStation.StationNumber == station.StationNumber);
            //insert
            DataSource.DataSource.StationsList.Add(station.Cloned());
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
            if (DataSource.DataSource.BusLinesList.Exists(currentBus => currentBus.LineNumber == busLine.LineNumber
            &&!currentBus.Deleted))
            {
                throw new BusException("The bus line already exists.");
                //return false;
            }
            BusLineDAO busLineDAO = busLine.Cloned();
            busLineDAO.CurrentSerialNB = Configuration.SerialBusLine;

            DataSource.DataSource.BusLinesList.Add(busLineDAO);
            return true;
        }
        /// <summary>
        /// remove a bus line from the bus lines list.
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        public bool RemoveBusLine(BusLineDAO busLine)
        {
            for (int i=0; i< DataSource.DataSource.BusLinesList.Count; i++)
            {
                var currentBusLine = DataSource.DataSource.BusLinesList[i];
                if (currentBusLine.CurrentSerialNB == busLine.CurrentSerialNB)
                {
                    var list = GetAllStationsLineOfBusLine(busLine.LineNumber);
                    for (int j=0; j < list.Count(); j++)
                    {
                        var item = list.ToList()[j];
                        RemoveStationLine(item);
                    }
                    currentBusLine.Deleted = true;
                    return true;
                }
            }

            //foreach (var currentBusLine in DataSource.DataSource.BusLinesList)
            //{
            //    if (currentBusLine.CurrentSerialNB == busLine.CurrentSerialNB)
            //    {
            //        foreach (var item in GetAllStationsLineOfBusLine(busLine.LineNumber))
            //        {
            //            RemoveStationLine(item);
            //        }
            //        currentBusLine.Deleted = true;

            //        //foreach (StationLineDAO currentStationLine in DataSource.StationLinesList)
            //        //{
            //        //    if(currentStationLine.LineNumber == currentBusLine.LineNumber)
            //        //    {
            //        //        currentStationLine.Deleted = true;
            //        //    }
            //        //}
            //        return true;
            //    }
            //}
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
            result = DataSource.DataSource.BusLinesList.FirstOrDefault(currentBusLine => currentBusLine.LineNumber == lineNumber);
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
            IEnumerable<BusLineDAO> result = from currentBusLine in DataSource.DataSource.BusLinesList
                                             //where currentBusLine.Deleted == false
                                             select currentBusLine.Cloned();
            return result;
        }
        /// <summary>
        /// bus line update
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        public bool UpdateBusLine(BusLineDAO busLine)
        {
            if (!DataSource.DataSource.BusLinesList.Exists(currentBusLine => currentBusLine.CurrentSerialNB == busLine.CurrentSerialNB))
            {
                return false;
            }
            //delete
            DataSource.DataSource.BusLinesList.RemoveAll(currentBusLine => currentBusLine.CurrentSerialNB == busLine.CurrentSerialNB);
            //insert
            DataSource.DataSource.BusLinesList.Add(busLine.Cloned());
            return true;
        }
        #endregion


        #region CoupleStationInRowDAO functions
        /// <summary>
        /// gets list of all the couple stations in a row
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CoupleStationInRowDAO> GetAllCoupleStationInRow()
        {
            //foreach (BusLineDAO busLine in GetAllBusLines())
            //{
            //    GetCoupleStationInRowOfOneBusLine(busLine);
            //}
            return from item in DataSource.DataSource.CoupleStationInRowList
                   select item.Cloned();
        }
        public CoupleStationInRowDAO GetOneCoupleStationInRow(int stationNumberOne,int stationNumberTwo)
        {
            foreach (var item in DataSource.DataSource.CoupleStationInRowList)
            {
                if (item.StationNumberOne == stationNumberOne
                    && item.StationNumberTwo == stationNumberTwo)
                {
                    return item;
                }
            }
            return null;
        }
        public bool AddCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow)
        {
            if(!DataSource.DataSource.CoupleStationInRowList.Exists(s=>s.StationNumberOne==coupleStationInRow.StationNumberOne
            && s.StationNumberTwo==coupleStationInRow.StationNumberTwo))
            {
                DataSource.DataSource.CoupleStationInRowList.Add(coupleStationInRow);
                return true;
            }
            return false;
        }
        public bool RemoveCoupleStationIRow(CoupleStationInRowDAO coupleStationInRow)
        {
                for (int i = 0; i < DataSource.DataSource.CoupleStationInRowList.Count; i++)
                {
                    var item = DataSource.DataSource.CoupleStationInRowList[i];
                    if (item.StationNumberOne == coupleStationInRow.StationNumberOne
                        && item.StationNumberTwo == coupleStationInRow.StationNumberTwo)
                    {
                        DataSource.DataSource.CoupleStationInRowList.RemoveAt(i);
                        return true;
                    }
                }
            return false;
        }
        public IEnumerable<CoupleStationInRowDAO> GetCoupleStationInRowDAOInBusLine(BusLineDAO busLine)
        {
            var w = GetAllStationsLineOfBusLine(busLine.LineNumber);
            IEnumerable<CoupleStationInRowDAO> list = new List<CoupleStationInRowDAO>();
            foreach (var item in GetAllCoupleStationInRow())
            {
                for (int i = 0; i < w.Count() - 1; i++)
                {
                    if (w.ToList()[i].StationNumber == item.StationNumberOne
                        && w.ToList()[i + 1].StationNumber == item.StationNumberTwo)
                    {
                        list.ToList().Add(item);
                    }
                }
            }
            return list;
        }

        //public void GetCoupleStationInRowOfOneBusLine(BusLineDAO busLine)
        //{
        //    //Random rand = new Random();
        //    IEnumerable<StationLineDAO> stationLines = GetAllStationsLineOfBusLine(busLine.LineNumber);
        //    //for (int i = 0; i < stationLines.Count() - 1; i++)
        //    //{
        //    //    CoupleStationInRowDAO stationInRow = new CoupleStationInRowDAO
        //    //    {
        //    //        StationNumberOne = stationLines.ElementAt(i).StationNumber,
        //    //        StationNumberTwo = stationLines.ElementAt(i + 1).StationNumber,
        //    //        AverageTravelTime = new TimeSpan(rand.Next(0), rand.Next(20), rand.Next(59)),
        //    //        Distance = rand.Next(6000)
        //    //    };
        //    //    if (!DataSource.DataSource.CoupleStationInRowList.Exists(s => s.StationNumberOne == stationInRow.StationNumberOne
        //    //     && s.StationNumberTwo == stationInRow.StationNumberTwo))
        //    //    {
        //    //        DataSource.DataSource.CoupleStationInRowList.Add(stationInRow.Cloned());
        //    //    }
        //    }

        //}

        /// <summary>
        /// couple station in a row update
        /// </summary>
        /// <param name="stationNumberOne"></param>
        /// <param name="stationNumberTwo"></param>
        /// <param name="time"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool UpdateCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow)
        {
            if (!DataSource.DataSource.CoupleStationInRowList.Exists(currentCoupleStation => currentCoupleStation.StationNumberOne == coupleStationInRow.StationNumberOne
             && currentCoupleStation.StationNumberTwo == coupleStationInRow.StationNumberTwo))
            {
                throw new BusException("This couple stations in row does not exists in the system.");
            }
            //CoupleStationInRowDAO coupleStation = new CoupleStationInRowDAO
            //{
            //    StationNumberOne = coupleStationInRow.StationNumberTwo,
            //    StationNumberTwo = coupleStationInRow.StationNumberTwo,
            //    AverageTravelTime = coupleStationInRow.AverageTravelTime,
            //    Distance = coupleStationInRow.Distance
            //};
            DataSource.DataSource.CoupleStationInRowList.RemoveAll(currentCoupleStation => currentCoupleStation.StationNumberOne == coupleStationInRow.StationNumberOne
            && currentCoupleStation.StationNumberTwo == coupleStationInRow.StationNumberTwo);

            DataSource.DataSource.CoupleStationInRowList.Add(coupleStationInRow.Cloned());
            return true;
        }
        #endregion


        #region LineInServiceDAO functions
        /// <summary>
        /// this func adds a new line in service to the line in service list.
        /// </summary>
        /// <param name="lineInService"></param>
        /// <returns></returns>
        public bool AddLineInService(LineInServiceDAO lineInService)
        {
            if (DataSource.DataSource.LineInServicesList.Exists(currentLineInService => currentLineInService.LineNumber == lineInService.LineNumber))
            {
                throw new BusException("Line in service already exists in the system.");
                //return false;
            }
            LineInServiceDAO cloned = lineInService.Cloned();
            cloned.LineInServiceSerialNB = Configuration.SerialLineInService;
            DataSource.DataSource.LineInServicesList.Add(cloned);
            return true;
        }
        /// <summary>
        /// this func removes a line in service from the line in service list.
        /// </summary>
        /// <param name="lineInService"></param>
        /// <returns></returns>
        public bool RemoveLineInService(LineInServiceDAO lineInService)
        {
            if (!DataSource.DataSource.LineInServicesList.Exists(currentLineInService => currentLineInService.LineNumber == lineInService.LineNumber))
            {
                return false;
            }
            //delete
            DataSource.DataSource.LineInServicesList.RemoveAll(currentLineInService => currentLineInService.LineNumber == lineInService.LineNumber);
            return true;
        }
        /// <summary>
        /// this func update a line in service in the line in service list.
        /// </summary>
        /// <param name="lineInService"></param>
        /// <returns></returns>
        public bool UpdateLineInService(LineInServiceDAO lineInService)
        {
            if (!DataSource.DataSource.LineInServicesList.Exists(currentLineInService => currentLineInService.LineNumber == lineInService.LineNumber))
            {
                return false;
            }
            //delete
            DataSource.DataSource.LineInServicesList.RemoveAll(currentLineInService => currentLineInService.LineNumber == lineInService.LineNumber);
            DataSource.DataSource.LineInServicesList.Add(lineInService.Cloned());
            return true;
        }
        #endregion


        #endregion
    }
}

