using BO;
using G_DALAPI;
using I_DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace F_BuisnessLayer
{
    class BLimp : IBL
    {
        IDal dal = DalFactory.GetDL();

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
            int lineNumber = busLineDAO.LineNumber;
            //try
            //{
            //    personDO = dl.GetPerson(licenseNumber);
            //}
            //catch (DO.BadPersonIdException ex)
            //{
            //    throw new BO.BadStudentIdException("Student ID is illegal", ex);
            //}
            busLineDAO.CopyPropertiesTo(busLineBO);
            //busDAO.CopyPropertiesTo(busBO);
            //busBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == licenseNumber)
            //                          let course = dl.GetCourse(sic.CourseId)
            //                          select course.CopyToStudentCourse(sic);
            return busLineBO;
        }
        public bool AddBusLine(BusLineBO busLine)
        {
            DO.BusLineDAO busLineDAO = new DO.BusLineDAO();
            busLine.CopyPropertiesTo(busLineDAO);
            try
            {
                if (dal.AddBusLine(busLineDAO))
                {
                    busLineDAO.Deleted = false;
                    busLineDAO.CurrentSerialNB = Configuration.SerialBusLine;

                    StationLineBO firstStation = busLine.StationLines.ElementAt(0);
                    firstStation.NumberStationInLine = 0;
                    StationLineBO lastStation = busLine.StationLines.ElementAt(1);
                    lastStation.NumberStationInLine = 1;

                    AddStationLine(firstStation);
                    AddStationLine(lastStation);
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
            busLine.CopyPropertiesTo(busLineDAO);
            try
            {
                if (dal.RemoveBusLine(busLineDAO))
                {
                    busLineDAO.Deleted = true;
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
                busLine.StationLines = from item in dal.GetAllStationsLineOfBusLine(busLine.LineNumber)
                                       select StationLineDoBoAdapter(item);
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
        public bool UpdateBusLine(BusLineBO busLine)
        {
            DO.BusLineDAO busLineDAO = new DO.BusLineDAO();
            busLine.CopyPropertiesTo(busLineDAO);
            try
            {
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
            stationLine.CopyPropertiesTo(stationLineDAO);
            try
            {
                if (dal.AddStationLine(stationLineDAO))
                {
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
            try
            {
                if (dal.RemoveStationLine(stationLineDAO))
                {
                    stationLineDAO.Deleted = true;
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
            return list.OrderBy(s => s.NumberStationInLine);
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
                    stationDAO.Deleted = false;
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
            station.CopyPropertiesTo(stationDAO);
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
                       select StationDoBoAdapter(item);
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
