using BO;
using G_DALAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace F_BuisnessLayer
{
    class BLimp : IBL
    {
        IDal dal = DalFactory.GetDL();
        #region BusBO
        BusBO busDoBoAdapter(DO.BusDAO busDAO)
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



    }
}
