using BO;
using BuisnessLayer;
using DALAPI;
using DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace BuisnessLayer
{
    class BLimp : IBL
    {
        IDal dal = DLFactory.GetDL();

        #region singleton parts
        private static IBL instance = new BLimp();
        public static IBL getInstance()
        {
            return instance;
        }
        static BLimp() { }
        private BLimp() { }
        #endregion end of singleton parts


        #region BusBO
        public BusBO busDoBoAdapter(DO.BusDAO busDAO)
        {
            BusBO busBO = new BusBO();
            //DO.Person personDO;
            string licenseNumber = busDAO.LicenseNumber;
            //try
            //{

            //    personDO = dl.GetPerson(licenseNumber);
            //}
            //catch (DO.BadPersonIdException ex)
            //{
            //    throw new BO.BadStudentIdException("Student ID is illegal", ex);
            //}
            busDAO.CopyPropertiesTo(busBO);

            //busDAO.CopyPropertiesTo(busBO);


            //busBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == licenseNumber)
            //                          let course = dl.GetCourse(sic.CourseId)
            //                          select course.CopyToStudentCourse(sic);
            return busBO;
        }
        public bool AddBus(BusBO bus)
        {
            DO.BusDAO busDAO = new DO.BusDAO();
            bus.CopyPropertiesTo(busDAO);
            try
            {
                if (dal.AddBus(busDAO))
                {
                    busDAO.Deleted = false;
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            //    if (DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            //    {
            //        throw new BusException("License exists allready.");
            //        //return false;
            //    }
            //BusDAO cloned = bus.Cloned();
            //DataSource.BussesList.Add(cloned);
            return true;
        }
        public bool RemoveBus(BusBO bus)
        {
            DO.BusDAO busDAO = new DO.BusDAO();
            bus.CopyPropertiesTo(busDAO);
            try
            {
                if (dal.RemoveBus(busDAO))
                {
                    busDAO.Deleted = true;
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public BusBO GetBusBO(string licenseNumber)
        {
            DO.BusDAO busDAO = new DO.BusDAO();
            try
            {
                busDAO = dal.GetOneBus(licenseNumber);

            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return busDoBoAdapter(busDAO);
        }
        public IEnumerable<BusBO> GetAllBusesBO()
        {
            return from item in dal.GetAllBusses()
                   where item.Deleted == false
                   select busDoBoAdapter(item);
        }
        public bool UpdateBus(BusBO bus)
        {
            DO.BusDAO busDAO = new DO.BusDAO();
            bus.CopyPropertiesTo(busDAO);
            try
            {
                if (dal.UpdateBus(busDAO))
                {
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        #endregion


        #region BusLineBo
        public BusLineBO busLineDoBoAdapter(DO.BusLineDAO busLineDAO)
        {
            BusLineBO busLineBO = new BusLineBO();
            //DO.Person personDO;
            //int lineNumber = busLineDAO.LineNumber;
            //try
            //{
            //    personDO = dl.GetPerson(licenseNumber);
            //}
            //catch (DO.BadPersonIdException ex)
            //{
            //    throw new BO.BadStudentIdException("Student ID is illegal", ex);
            //}
            busLineDAO.CopyPropertiesTo(busLineBO);
            busLineBO.StationLines = from item in dal.GetAllStationsLineOfBusLine(busLineBO.LineNumber)
                                     select StationLineDoBoAdapter(item);
            //busDAO.CopyPropertiesTo(busBO);
            //busBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == licenseNumber)
            //                          let course = dl.GetCourse(sic.CourseId)
            //                          select course.CopyToStudentCourse(sic);
            return busLineBO;
        }
        public bool AddBusLine(BusLineBO busLine,StationLineBO firstStationLineBO,StationLineBO lastStationLineBO)
        {
            DO.BusLineDAO busLineDAO = new DO.BusLineDAO();
            //busLineDAO.FirstStationNumber = busLine.StationLines.ElementAt(0).Station.StationNumber;
            //busLineDAO.LastStationNumber = busLine.StationLines.ElementAt(busLine.StationLines.Count()).Station.StationNumber;
            busLine.CopyPropertiesTo(busLineDAO);
            try
            {
                if (dal.AddBusLine(busLineDAO))
                {
                    busLineDAO.Deleted = false;
                    AddStationLine(firstStationLineBO);
                    AddStationLine(lastStationLineBO);
                    busLine.StationLines = from item in GetAllStationLines(busLine.LineNumber)
                                             select item;
                    //StationLineBO firstStation = busLine.StationLines.ElementAt(0);
                    //firstStation.NumberStationInLine = 0;
                    //StationLineBO lastStation = busLine.StationLines.ElementAt(1);
                    //lastStation.NumberStationInLine = 1;

                    //AddStationLine(firstStation);
                    //AddStationLine(lastStation);
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            //    if (DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            //    {
            //        throw new BusException("License exists allready.");
            //        //return false;
            //    }
            //BusDAO cloned = bus.Cloned();
            //DataSource.BussesList.Add(cloned);
            return true;
        }
        public bool RemoveBusLine(BusLineBO busLine)
        {
            DO.BusLineDAO busLineDAO = new DO.BusLineDAO();
            busLineDAO = dal.GetOneBusLine(busLine.LineNumber);
            //GetOneBusLine(busLine.LineNumber).CopyPropertiesTo(busLineDAO);
            //busLineDAO.FirstStationNumber = busLine.StationLines.ElementAt(0).Station.StationNumber;
            //busLineDAO.LastStationNumber = busLine.StationLines.ElementAt(busLine.StationLines.Count()-1).Station.StationNumber;
            //busLine.CopyPropertiesTo(busLineDAO);
            try
            {
                if (dal.RemoveBusLine(busLineDAO))
                {
                    //foreach (var item in busLine.StationLines)
                    //{
                    //    RemoveStationLine(item);
                    //}
                    //busLineDAO.Deleted = true;
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public BusLineBO GetOneBusLine(int lineNumber)
        {
            DO.BusLineDAO busLineDAO = new DO.BusLineDAO();
            BusLineBO busLine = new BusLineBO();
            try
            {
                busLineDAO = dal.GetOneBusLine(lineNumber);
                busLine = busLineDoBoAdapter(busLineDAO);
                busLine.StationLines = from item in GetAllStationLines(busLine.LineNumber)
                                       select item;
                //int i = 0;
                //foreach (var item in dal.GetAllCoupleStationInRow())
                //{
                //    if (item.StationNumberOne == busLine.StationLines.ElementAt(i).Station.StationNumber
                //        && item.StationNumberTwo == busLine.StationLines.ElementAt(i+1).Station.StationNumber)
                //    {
                //        //busLine.StationLines.ElementAt(i).LineNumber = item.Distance;
                //        StationLineBO stationLineBO = StationLineDoBoAdapter(dal.GetOneStationLine(busLine.LineNumber, busLine.StationLines.ElementAt(i).Station.StationNumber));
                //        stationLineBO.Distance = item.Distance;
                //        stationLineBO.Time = item.AverageTravelTime;
                //        UpdateStationLine(stationLineBO);

                //        //busLine.StationLines.ElementAt(i).Distance = item.Distance;
                //        //busLine.StationLines.ElementAt(i).Time = item.AverageTravelTime;
                //        i++;
                //        if(busLine.StationLines.Count()-1==i)
                //        {
                //            break;
                //        }
                //    }
                //}
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return busLine;
        }
        public IEnumerable<BusLineBO> GetAllBusLines()
        {
            var list = from item in dal.GetAllBusLines()
                       where item.Deleted == false
                       select GetOneBusLine(item.LineNumber);
            return list.OrderBy(s => s.LineNumber);
        }
        public bool UpdateBusLine(BusLineBO busLine, StationLineBO firstStationLineBO, StationLineBO lastStationLineBO)
        {
            DO.BusLineDAO busLineDAO = new DO.BusLineDAO();
            busLine.CopyPropertiesTo(busLineDAO);
            try
            {
                if (!DataSource.DataSource.StationLinesList.Exists(s => s.StationNumber == firstStationLineBO.Station.StationNumber
                && s.LineNumber==firstStationLineBO.LineNumber))
                {
                    AddStationLine(firstStationLineBO);
                }
                if (!DataSource.DataSource.StationLinesList.Exists(s => s.StationNumber == lastStationLineBO.Station.StationNumber
                && s.LineNumber==lastStationLineBO.LineNumber))
                {
                    AddStationLine(lastStationLineBO);
                }
                if (dal.UpdateBusLine(busLineDAO))
                {
                    foreach (var item in dal.GetAllStationsLineOfBusLine(busLineDAO.LineNumber))
                    {
                        dal.UpdateStationLine(item);
                    }
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        #endregion


        #region StationLineBO
        public StationLineBO StationLineDoBoAdapter(DO.StationLineDAO stationLineDAO)
        {
            StationLineBO stationLineBO = new StationLineBO();
            //DO.Person personDO;
            //int lineNumber = stationLineDAO.LineNumber;
            //try
            //{
            //    personDO = dl.GetPerson(licenseNumber);
            //}
            //catch (DO.BadPersonIdException ex)
            //{
            //    throw new BO.BadStudentIdException("Student ID is illegal", ex);
            //}
            stationLineBO.Station = StationDoBoAdapter(dal.GetOneStation(stationLineDAO.StationNumber));
            stationLineDAO.CopyPropertiesTo(stationLineBO);
            //busDAO.CopyPropertiesTo(busBO);
            //busBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == licenseNumber)
            //                          let course = dl.GetCourse(sic.CourseId)
            //                          select course.CopyToStudentCourse(sic);
            return stationLineBO;
        }
        public bool AddStationLine(StationLineBO stationLine)
        {
            DO.StationLineDAO stationLineDAO = new DO.StationLineDAO();
            stationLineDAO.StationNumber = stationLine.Station.StationNumber;
            stationLine.CopyPropertiesTo(stationLineDAO);
            try
            {
                IEnumerable<StationLineBO> list = new List<StationLineBO>(GetAllStationLines(stationLine.LineNumber));
                if (dal.AddStationLine(stationLineDAO))
                {
                    Random rand = new Random();
                    int distance = rand.Next(1000, 20000);
                    dal.AddCoupleStationInRow(new DO.CoupleStationInRowDAO
                    {
                        Distance = distance,
                        AverageTravelTime = new TimeSpan(0, distance / 1000, rand.Next(59)),
                        StationNumberOne = list.ToList()[stationLine.NumberStationInLine - 1].Station.StationNumber,
                        StationNumberTwo = stationLine.Station.StationNumber
                    });
                    dal.AddCoupleStationInRow(new DO.CoupleStationInRowDAO
                    {
                        AverageTravelTime = stationLine.Time,
                        Distance = stationLine.Distance,
                        StationNumberOne = stationLine.Station.StationNumber,
                        StationNumberTwo = list.ToList()[stationLine.NumberStationInLine].Station.StationNumber
                    });
                    stationLineDAO.Deleted = false;
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public bool RemoveStationLine(StationLineBO stationLine)
        {
            DO.StationLineDAO stationLineDAO = new DO.StationLineDAO();
            stationLine.CopyPropertiesTo(stationLineDAO);
            stationLineDAO.StationNumber = stationLine.Station.StationNumber;
            try
            {
                if (dal.RemoveStationLine(stationLineDAO))
                {
                    //stationLineDAO.Deleted = true;
                    if (stationLine.NumberStationInLine > 0)
                    {
                        foreach (var item in GetAllStationLines(stationLine.LineNumber))
                        {
                            if (stationLine.NumberStationInLine == GetAllStationLines(stationLine.LineNumber).Count()
                                && item.NumberStationInLine + 1 == stationLine.NumberStationInLine)
                            {
                                item.Time = new TimeSpan(0, 0, 0);
                                item.Distance = 0;
                            }
                            else if (item.NumberStationInLine + 1 == stationLine.NumberStationInLine)
                            {
                                item.Time += stationLine.Time;
                                item.Distance += stationLine.Distance;
                            }
                        }
                    }
                    
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public bool UpdateStationLine(StationLineBO stationLine)
        {
            DO.StationLineDAO stationLineDAO = new DO.StationLineDAO();
            stationLine.CopyPropertiesTo(stationLineDAO);
            try
            {
                if (dal.UpdateStationLine(stationLineDAO))
                {
                    AddStationLine(stationLine);
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public IEnumerable<StationLineBO> GetAllStationLines(int num)
        {
            IEnumerable<StationLineBO> list = from item in dal.GetAllStationsLineOfBusLine(num)
                                              where item.LineNumber == num && !item.Deleted
                                              select StationLineDoBoAdapter(item);
            //int i = 0; i < list.Count() - 1; i++
            List<StationLineBO> list1 = new List<StationLineBO>();
            int i = 0;
            for (; i < list.Count() - 1; i++)
            {
                foreach (var item in dal.GetAllCoupleStationInRow())
                {
                    if (list.ToList()[i].Station.StationNumber == item.StationNumberOne
                        && list.ToList()[i + 1].Station.StationNumber == item.StationNumberTwo)
                    {
                        //list.ToList()[i].Distance = item.Distance;
                        //list.ToList()[i].Time = item.AverageTravelTime;
                        StationLineBO stationLineBO = new StationLineBO
                        {
                            Distance = item.Distance,
                            Time = item.AverageTravelTime,
                            Deleted = false,
                            LineNumber = list.ToList()[i].LineNumber,
                            NumberStationInLine = list.ToList()[i].NumberStationInLine,
                            Station = list.ToList()[i].Station
                        };
                        //UpdateStationLine(stationLineBO);
                        if (!list1.Exists(s => s.Station.StationNumber == stationLineBO.Station.StationNumber))
                        {
                            list1.Add(stationLineBO);
                        }
                    }
                }
                // i++;
            }
            list1.Add(list.ToList()[i]);
            list1.OrderBy(s => s.NumberStationInLine);
            list1.Last().Distance = 0;
            list1.Last().Time = new TimeSpan(0, 0, 0);
            return list1;
        }
        #endregion


        #region StationBO
        public StationBO StationDoBoAdapter(DO.StationDAO stationDAO)
        {
            StationBO stationBO = new StationBO();
            //DO.Person personDO;
            //int lineNumber = stationDAO.LineNumber;
            //try
            //{
            //    personDO = dl.GetPerson(licenseNumber);
            //}
            //catch (DO.BadPersonIdException ex)
            //{
            //    throw new BO.BadStudentIdException("Student ID is illegal", ex);
            //}
            stationDAO.CopyPropertiesTo(stationBO);
            //busDAO.CopyPropertiesTo(busBO);
            //busBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == licenseNumber)
            //                          let course = dl.GetCourse(sic.CourseId)
            //                          select course.CopyToStudentCourse(sic);
            return stationBO;
        }
        public bool AddStation(StationBO station)
        {
            DO.StationDAO stationDAO = new DO.StationDAO();
            station.CopyPropertiesTo(stationDAO);
            try
            {
                if (dal.AddStation(stationDAO))
                {
                    //stationDAO.Deleted = false;
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public bool RemoveStation(StationBO station)
        {
            DO.StationDAO stationDAO = new DO.StationDAO();
            stationDAO = dal.GetOneStation(station.StationNumber);
            //station.CopyPropertiesTo(stationDAO);
            try
            {
                if (dal.RemoveStation(stationDAO))
                {
                    stationDAO.Deleted = true;
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public bool UpdateStation(StationBO station)
        {
            DO.StationDAO stationDAO = new DO.StationDAO();
            station.CopyPropertiesTo(stationDAO);
            try
            {
                if (dal.UpdateStation(stationDAO))
                {
                    return true;
                }
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return true;
        }
        public StationBO GetOneStation(int stationNumber)
        {
            DO.StationDAO stationDAO = new DO.StationDAO();
            StationBO station = new StationBO();
            try
            {
                stationDAO = dal.GetOneStation(stationNumber);
                station = StationDoBoAdapter(stationDAO);
            }
            catch (Exception be)
            {
                throw new BusExeption(be.Message);
            }
            return station;
        }
        public IEnumerable<StationBO> GetAllStations()
        {
            var list = from item in dal.GetAllStations()
                       where item.Deleted == false
                       select GetOneStation(item.StationNumber);
            return list.OrderBy(s => s.StationNumber);
        }
        public IEnumerable<BusLineBO> GetAllBusLinesInStation(int stationNumber)
        {
            IEnumerable<BusLineBO> list = from item in dal.GetAllStationLines()
                       where item.StationNumber == stationNumber
                       select GetOneBusLine(item.LineNumber);
            return list;
        }
        #endregion


    }
}
