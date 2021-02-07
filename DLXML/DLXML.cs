using DAL;
using DALAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DAL
{
    sealed class DLXML : IDal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion


        #region DS XML Files
        string stationsPath = @"Stations.xml";
        string stationLinePath = @"StationLine.xml";
        string coupleStationInRowPath = @"CoupleStation.xml";

        #endregion


        #region StationDAO
        public DO.StationDAO GetOneStation(int stationNumber)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            StationDAO station = (from sta in stationsRootElem.Elements()
                                  where int.Parse(sta.Element("StationNumber").Value) == stationNumber
                                  select new StationDAO()
                                  {
                                      StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                                      StationName = sta.Element("StationName").Value,
                                      Longtitude = double.Parse(sta.Element("Longtitude").Value),
                                      Latitude = double.Parse(sta.Element("Latitude").Value),
                                      Deleted = bool.Parse(sta.Element("Deleted").Value)
                                  }
                        ).FirstOrDefault();
            if (station == null)
            {
                //throw new DO.BadPersonIdException(stationNumber, $"bad person id: {stationNumber}");
                return null;
            }
            return station;
        }

        public IEnumerable<DO.StationDAO> GetAllStations()
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            return from sta in stationsRootElem.Elements()
                   select new StationDAO()
                   {
                       StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                       StationName = sta.Element("StationName").Value,
                       Longtitude = double.Parse(sta.Element("Longtitude").Value),
                       Latitude = double.Parse(sta.Element("Latitude").Value),
                       Deleted = bool.Parse(sta.Element("Deleted").Value)
                   };
        }

        //public IEnumerable<DO.Person> GetAllPersonsBy(Predicate<DO.Person> predicate)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

        //    return from p in personsRootElem.Elements()
        //           let p1 = new Person()
        //           {
        //               ID = Int32.Parse(p.Element("ID").Value),
        //               Name = p.Element("Name").Value,
        //               Street = p.Element("Street").Value,
        //               HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
        //               City = p.Element("City").Value,
        //               BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
        //               PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value),
        //               Duration = TimeSpan.ParseExact(p.Element("Duration").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
        //           }
        //           where predicate(p1)
        //           select p1;
        //}

        public bool AddStation(DO.StationDAO stations)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement sta1 = (from st in stationsRootElem.Elements()
                             where int.Parse(st.Element("StationNumber").Value) == stations.StationNumber
                             select st).FirstOrDefault();
            if (sta1 != null)
            {
                //throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");
                return true;
            }
            XElement stationElem = new XElement("StationDAO", new XElement("StationNumber", Int32.Parse(sta1.Element("StationNumber").Value),
                new XElement("StationName", sta1.Element("StationName").Value),
                new XElement("Longtitude", double.Parse(sta1.Element("Longtitude").Value)),
                new XElement("Latitude", double.Parse(sta1.Element("Latitude").Value)),
                new XElement("Deleted", bool.Parse(sta1.Element("Deleted").Value))));
            stationsRootElem.Add(stationElem);
            XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);
            return true;
        }

        public bool RemoveStation(int StationNumber)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement sta1 = (from sta in stationsRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                sta1.Element("Deleted").Value = true.ToString();
                //for (int j = 0; j < DataSource.DataSource.StationLinesList.Count; j++)
                //{
                //    var item = DataSource.DataSource.StationLinesList[j];
                //    if (item.StationNumber == station.StationNumber)
                //    {
                //        RemoveStationLine(item);
                //    }
                XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(StationNumber, $"bad person id: {StationNumber}");
            }
            return true;
        }

        public bool UpdateStation(DO.StationDAO station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement sta1 = (from sta in stationsRootElem.Elements()
                            where int.Parse(sta.Element("StationNumber").Value) == station.StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                sta1.Element("StationNumber").Value = station.StationNumber.ToString();
                sta1.Element("StationName").Value = station.StationName;
                sta1.Element("Longtitude").Value = station.Longtitude.ToString();
                sta1.Element("Latitude").Value = station.Latitude.ToString();
                sta1.Element("Deleted").Value = station.Deleted.ToString();
                XMLTools.SaveListToXMLElement(stationsRootElem, stationsPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(station.ID, $"bad person id: {station.stationNumber}");
                return true;
            }
            return true;
        }
        #endregion


        #region CoupleStationInRow
        public IEnumerable<DO.CoupleStationInRowDAO> GetAllCoupleStationInRow()
        {
            XElement coupleStationRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            return from coupleSta in coupleStationRootElem.Elements()
                   select new CoupleStationInRowDAO()
                   {
                       StationNumberOne = Int32.Parse(coupleSta.Element("StationNumberOne").Value),
                       StationNumberTwo = Int32.Parse(coupleSta.Element("StationNumberTwo").Value),
                       Distance = Int32.Parse(coupleSta.Element("Distance").Value),
                       AverageTravelTime = TimeSpan.Parse(coupleSta.Element("AverageTravelTime").Value),
                   };
        }

        public DO.CoupleStationInRowDAO GetOneCoupleStationInRow(int stationNumberOne, int stationNumberTwo)
        {
            XElement coupleStationRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            CoupleStationInRowDAO coupleStation = (from coupleSta in coupleStationRootElem.Elements()
                                  where int.Parse(coupleSta.Element("StationNumberOne").Value) == stationNumberOne
                                  && int.Parse(coupleSta.Element("StationNumberTwo").Value) == stationNumberTwo
                                        select new CoupleStationInRowDAO()
                                        {
                                            StationNumberOne = Int32.Parse(coupleSta.Element("StationNumberOne").Value),
                                            StationNumberTwo = Int32.Parse(coupleSta.Element("StationNumberTwo").Value),
                                            Distance = Int32.Parse(coupleSta.Element("Distance").Value),
                                            AverageTravelTime = TimeSpan.Parse(coupleSta.Element("AverageTravelTime").Value)
                                        }).FirstOrDefault();

            if (coupleStation == null)
            {
                //throw new DO.BadPersonIdException(stationNumber, $"bad person id: {stationNumber}");
                return null;
            }
            return coupleStation;
        }

        public bool AddCoupleStationInRow(DO.CoupleStationInRowDAO coupleStation)
        {
            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            XElement coupleStationInRow1 = (from coupleSta in coupleStationInRowRootElem.Elements()
                             where int.Parse(coupleSta.Element("StationNumberOne").Value) == coupleStation.StationNumberOne
                             && int.Parse(coupleSta.Element("StationNumberTwo").Value) == coupleStation.StationNumberTwo
                                            select coupleSta).FirstOrDefault();
            if (coupleStationInRow1 != null)
            {
                //throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");
                return true;
            }
            XElement coupleStationElem = new XElement("CoupleStationInRowDAO", new XElement("StationNumberOne", Int32.Parse(coupleStationInRow1.Element("StationNumberOne").Value),
                new XElement("StationNumberTwo", coupleStationInRow1.Element("StationNumberTwo").Value),
                new XElement("Distance", Int32.Parse(coupleStationInRow1.Element("Distance").Value)),
                new XElement("AverageTravelTime", TimeSpan.Parse(coupleStationInRow1.Element("AverageTravelTime").Value))));
            coupleStationInRowRootElem.Add(coupleStationElem);
            XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, stationsPath);
            return true;
        }

        public bool RemoveCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow)
        {
            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            XElement coupleStation1 = (from coupleSta in coupleStationInRowRootElem.Elements()
                                       where int.Parse(coupleSta.Element("StationNumberOne").Value) == coupleStation.StationNumberOne
                                       && int.Parse(coupleSta.Element("StationNumberTwo").Value) == coupleStation.StationNumberTwo
                                       select coupleSta).FirstOrDefault();

            if (coupleStation1 != null)
            {
                coupleStation1.Element("Deleted").Value = true.ToString();
                //for (int j = 0; j < DataSource.DataSource.StationLinesList.Count; j++)
                //{
                //    var item = DataSource.DataSource.StationLinesList[j];
                //    if (item.StationNumber == station.StationNumber)
                //    {
                //        RemoveStationLine(item);
                //    }
                XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, coupleStationInRowPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(StationNumber, $"bad person id: {StationNumber}");
                return true;
            }
            return true;
        }

        public bool UpdateCoupleStationInRow(DO.CoupleStationInRowDAO coupleStation)
        {
            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            XElement coupleStation1 = (from coupleSta in coupleStationInRowRootElem.Elements()
                                       where int.Parse(coupleSta.Element("StationNumberOne").Value) == coupleStation.StationNumberOne
                                       && int.Parse(coupleSta.Element("StationNumberTwo").Value) == coupleStation.StationNumberTwo
                                       select coupleSta).FirstOrDefault();
            if (coupleStation1 != null)
            {
                coupleStation1.Element("StationNumberOne").Value = coupleStation.StationNumberOne.ToString();
                coupleStation1.Element("StationNumberTwo").Value = coupleStation.StationNumberTwo.ToString();
                coupleStation1.Element("Distance").Value = coupleStation.Distance.ToString();
                coupleStation1.Element("AverageTravelTime").Value = coupleStation.AverageTravelTime.ToString();
                XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, stationsPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(station.ID, $"bad person id: {station.stationNumber}");
                return true;
            }
            return true;
        }
        #endregion

    }
}
