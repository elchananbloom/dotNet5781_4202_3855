using DAL;
using DALAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;

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
        string stationPath = @"Stations.xml";
        string stationLinePath = @"StationLine.xml";
        string coupleStationInRowPath = @"CoupleStation.xml";
        string busLinePath = @"BusLine.xml";

        #endregion


        #region StationDAO
        public DO.StationDAO GetOneStation(int stationNumber)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
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
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
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
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
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
            XMLTools.SaveListToXMLElement(stationsRootElem, stationPath);
            return true;
        }

        public bool RemoveStation(int stationNumber)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
            XElement sta1 = (from sta in stationsRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == stationNumber
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
                XMLTools.SaveListToXMLElement(stationsRootElem, stationPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(StationNumber, $"bad person id: {StationNumber}");
            }
            return true;
        }

        public bool UpdateStation(DO.StationDAO station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
            XElement sta1 = (from sta in stationsRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == station.StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                //foreach (var item in stationsRootElem.Elements())
                //{
                //    if (int.Parse(sta1.Element("StationNumber").Value) == item.StationNumber)
                //    {
                //        stationsRootElem.Remove();
                //    }
                //}
                sta1.Element("StationNumber").Value = station.StationNumber.ToString();
                sta1.Element("StationName").Value = station.StationName;
                sta1.Element("Longtitude").Value = station.Longtitude.ToString();
                sta1.Element("Latitude").Value = station.Latitude.ToString();
                sta1.Element("Deleted").Value = station.Deleted.ToString();
                XMLTools.SaveListToXMLElement(stationsRootElem, stationPath);
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
            XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, stationPath);
            return true;
        }

        public bool RemoveCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow)
        {
            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            XElement coupleStation1 = (from coupleSta in coupleStationInRowRootElem.Elements()
                                       where int.Parse(coupleSta.Element("StationNumberOne").Value) == coupleStationInRow.StationNumberOne
                                       && int.Parse(coupleSta.Element("StationNumberTwo").Value) == coupleStationInRow.StationNumberTwo
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
                XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, stationPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(station.ID, $"bad person id: {station.stationNumber}");
                return true;
            }
            return true;
        }
        #endregion


        #region Station Line
        public DO.StationLineDAO GetOneStationLine(int lineNumber, int stationNumber)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            StationLineDAO stationLine = (from sta in stationLineRootElem.Elements()
                                          where int.Parse(sta.Element("StationNumber").Value) == stationNumber
                                          && int.Parse(sta.Element("lineNumber").Value) == lineNumber
                                          select new StationLineDAO()
                                          {
                                              LineNumber = Int32.Parse(sta.Element("LineNumber").Value),
                                              StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                                              NumberStationInLine = Int32.Parse(sta.Element("NumberStationInLine").Value),
                                              Deleted = bool.Parse(sta.Element("Deleted").Value)
                                          }).FirstOrDefault();
            if (stationLine == null)
            {
                //throw new DO.BadPersonIdException(stationNumber, $"bad person id: {stationNumber}");
                return null;
            }
            return stationLine;
        }

        public IEnumerable<DO.StationLineDAO> GetAllStationLines()
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            return from sta in stationLineRootElem.Elements()
                   select new StationLineDAO()
                   {
                       LineNumber = Int32.Parse(sta.Element("LineNumber").Value),
                       StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                       NumberStationInLine = Int32.Parse(sta.Element("NumberStationInLine").Value),
                       Deleted = bool.Parse(sta.Element("Deleted").Value)
                   };
        }

        public IEnumerable<DO.StationLineDAO> GetAllStationsLineOfBusLine(int lineNumber)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            return from sta in stationLineRootElem.Elements()
                   where int.Parse(sta.Element("lineNumber").Value) == lineNumber
                   && !bool.Parse(sta.Element("Deleted").Value)
                   select new StationLineDAO()
                   {
                       LineNumber = Int32.Parse(sta.Element("LineNumber").Value),
                       StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                       NumberStationInLine = Int32.Parse(sta.Element("NumberStationInLine").Value),
                       Deleted = bool.Parse(sta.Element("Deleted").Value)
                   };
        }

        public bool UpdateStationLine(DO.StationLineDAO stationLine)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            XElement sta1 = (from sta in stationLineRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == stationLine.StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                sta1.Element("StationNumber").Value = stationLine.StationNumber.ToString();
                sta1.Element("LineNumber").Value = stationLine.LineNumber.ToString();
                sta1.Element("NumberStationInLine").Value = stationLine.NumberStationInLine.ToString();
                sta1.Element("Deleted").Value = stationLine.Deleted.ToString();
                XMLTools.SaveListToXMLElement(stationLineRootElem, stationPath);
            }
            else
            {
                //throw new DO.BadPersonIdException(station.ID, $"bad person id: {station.stationNumber}");
                return true;
            }
            return true;
        }

        public bool AddStationLine(DO.StationLineDAO stationLine)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            XElement sta1 = (from st in stationLineRootElem.Elements()
                             where int.Parse(st.Element("StationNumber").Value) == stationLine.StationNumber
                             && int.Parse(st.Element("lineNumber").Value) == stationLine.LineNumber
                             && !bool.Parse(st.Element("Deleted").Value)
                             select st).FirstOrDefault();
            if (sta1 != null)
            {
                //throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");
                return true;
            }
            IEnumerable<StationLineDAO> list = new List<StationLineDAO>(GetAllStationsLineOfBusLine(stationLine.LineNumber));
            for (int i = stationLine.NumberStationInLine; i < list.Count(); i++)
            {
                list.ToList()[i].NumberStationInLine = i + 1;
                //DataSource.DataSource.StationLinesList[i].NumberStationInLine = i - 1;
            }
            foreach (var item in list)
            {
                UpdateStationLine(item);
            }
            XElement stationLineElem = new XElement("StationLineDAO",
                new XElement("LineNumber", stationLine.LineNumber),
                new XElement("StationNumber", stationLine.StationNumber),
                new XElement("NumberStationInLine", stationLine.NumberStationInLine),
                new XElement("Deleted", stationLine.Deleted));
            stationLineRootElem.Add(stationLineElem);
            XMLTools.SaveListToXMLElement(stationLineRootElem, stationLinePath);
            return true;
        }

        public bool RemoveStationLine(StationLineDAO stationLine)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            XElement sta1 = (from sta in stationLineRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == stationLine.StationNumber
                             && int.Parse(sta.Element("StationLine").Value) == stationLine.StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                sta1.Element("Deleted").Value = true.ToString();
                foreach (var currentStationLine in stationLineRootElem.Elements())
                {
                    if (int.Parse(currentStationLine.Element("StationNumber").Value) == stationLine.StationNumber
                         && int.Parse(currentStationLine.Element("StationLine").Value) == stationLine.StationNumber)
                    {
                        currentStationLine.Element("Deleted").Value = true.ToString();
                        IEnumerable<StationLineDAO> list = new List<StationLineDAO>(GetAllStationsLineOfBusLine(stationLine.LineNumber));
                        for (int i = list.Count() - 1; i >= stationLine.NumberStationInLine; i--)
                        {
                            list.ToList()[i].NumberStationInLine = i;
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
                            XElement coupleStationElem = new XElement("CoupleStationInRowDAO", new XElement("StationNumberOne", Int32.Parse(coupleStation1.StationNumberOne.ToString())),
                                new XElement("StationNumberTwo", Int32.Parse(list.ToList()[stationLine.NumberStationInLine].StationNumber.ToString())),
                                 new XElement("Distance", Int32.Parse((coupleStation1.Distance + coupleStation2.Distance).ToString())),
                                 new XElement("AverageTravelTime", XmlConvert.ToTimeSpan((coupleStation1.AverageTravelTime + coupleStation2.AverageTravelTime).ToString())));


                            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
                            XElement newCoupleStation = (from sta in stationLineRootElem.Elements()
                                                         where int.Parse(sta.Element("StationNumber").Value) == stationLine.StationNumber
                                                         && int.Parse(sta.Element("StationLine").Value) == stationLine.StationNumber
                                                         select sta).FirstOrDefault();
                            if (newCoupleStation == null)
                            {
                                coupleStationInRowRootElem.Add(coupleStationElem);
                            }
                            foreach (var item in list)
                            {
                                UpdateStationLine(item);
                            }
                            return true;
                        }
                    }
                    //throw new BusException("The station line does not exists in the system.");
                }
            }
            return true;
        }
        #endregion


        #region BusLine
        public DO.BusLineDAO GetOneBusLine(int lineNumber)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            BusLineDAO busLine = (from busLin in busLineRootElem.Elements()
                                          where int.Parse(busLin.Element("lineNumber").Value) == lineNumber
                                          select new BusLineDAO()
                                          {
                                              CurrentSerialNB = Int32.Parse(busLin.Element("CurrentSerialNB").Value),
                                              LineNumber = Int32.Parse(busLin.Element("LineNumber").Value),
                                              FirstStationNumber = Int32.Parse(busLin.Element("FirstStationNumber").Value),
                                              LastStationNumber = Int32.Parse(busLin.Element("LastStationNumber").Value),
                                              /////////////////////////////////////////////////////////////////////////////////
                                              Area = Enum.Parse(Area, busLin.Element("Area").Value),
                                              Deleted = bool.Parse(busLin.Element("Deleted").Value)
                                          }).FirstOrDefault();
            if (busLine == null)
            {
                //throw new DO.BadPersonIdException(stationNumber, $"bad person id: {stationNumber}");
                return null;
            }
            return busLine;
        }

        public IEnumerable<DO.BusLineDAO> GetAllBusLine()
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            return from busLin in busLineRootElem.Elements()
                   select new BusLineDAO()
                   {
                       CurrentSerialNB = Int32.Parse(busLin.Element("CurrentSerialNB").Value),
                       LineNumber = Int32.Parse(busLin.Element("LineNumber").Value),
                       FirstStationNumber = Int32.Parse(busLin.Element("FirstStationNumber").Value),
                       LastStationNumber = Int32.Parse(busLin.Element("LastStationNumber").Value),
                       /////////////////////////////////////////////////////////////////////////////////
                       Area = Enum.Parse(Area, busLin.Element("Area").Value),
                       Deleted = bool.Parse(busLin.Element("Deleted").Value)
                   };
        }

        public bool AddBusLine(DO.BusLineDAO busLine)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            XElement busLine1 = (from busLin in busLineRootElem.Elements()
                             where int.Parse(busLin.Element("LineNumber").Value) == busLine.LineNumber
                             && !bool.Parse(busLin.Element("Deleted").Value)
                             select busLin).FirstOrDefault();
            if (busLine1 != null)
            {
                //throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");
                return true;
            }
            XElement busLineElem = new XElement("BusLineDAO", new XElement("LineNumber", Int32.Parse(busLine1.Element("LineNumber").Value),
                new XElement("CurrentSerialNB", Int32.Parse(busLine1.Element("CurrentSerialNB").Value)),
                new XElement("FirstStationNumber", Int32.Parse(busLine1.Element("FirstStationNumber").Value)),
                new XElement("LastStationNumber", Int32.Parse(busLine1.Element("LastStationNumber").Value)),
                ////////////////////////////////////////////////////////////////////////////
                new XElement("Area", Enum.Parse(busLine1.Element("Area").Value)),
                new XElement("Deleted", bool.Parse(busLine1.Element("Deleted").Value))));
            busLineRootElem.Add(busLineElem);
            XMLTools.SaveListToXMLElement(busLineRootElem, busLinePath);
            return true;
        }

        public bool RemoveBusLine(BusLineDAO busLine)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            IEnumerable<BusLineDAO> list = new List<BusLineDAO> (GetAllBusLine());
            for (int i = 0; i < GetAllBusLine().Count(); i++)
            {
                XElement currentBusLine = list.ElementAt(i);
                if (currentBusLine.CurrentSerialNB == busLine.CurrentSerialNB)
                {
                    var list1 = GetAllStationsLineOfBusLine(busLine.LineNumber);
                    for (int j = 0; j < list1.Count(); j++)
                    {
                        var item = list1.ToList()[j];
                        RemoveStationLine(item);
                    }
                    currentBusLine.Element("Deleted").Value = true.ToString();

                    XMLTools.SaveListToXMLElement(busLineRootElem, busLinePath);
                    return true;
                }
            }
            return true;
        }


        public bool RemoveBusLine(BusLineDAO busLine)
        {
            for (int i = 0; i < DataSource.DataSource.BusLinesList.Count; i++)
            {
                var currentBusLine = DataSource.DataSource.BusLinesList[i];
                if (currentBusLine.CurrentSerialNB == busLine.CurrentSerialNB)
                {
                    var list = GetAllStationsLineOfBusLine(busLine.LineNumber);
                    for (int j = 0; j < list.Count(); j++)
                    {
                        var item = list.ToList()[j];
                        RemoveStationLine(item);
                    }
                    currentBusLine.Deleted = true;
                    return true;
                }
            }
            throw new BusException("The bus line does not exists in the system.");
        }
        #endregion


    }









}


