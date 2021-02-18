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
        string stationLinePath = @"StationLines.xml";
        string coupleStationInRowPath = @"CoupleStationInRow.xml";
        string busLinePath = @"BusLines.xml";
        string busInTravelPath = @"BusInTravel.xml";
        string lineinServicePath = @"LineInService.xml";
        string busPath = @"Busses.xml";
        #endregion


        #region StationDAO
        public bool AddStation(DO.StationDAO stations)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
            XElement sta1 = (from st in stationsRootElem.Elements()
                             where int.Parse(st.Element("StationNumber").Value) == stations.StationNumber
                             select st).FirstOrDefault();
            if (sta1 != null)
            {
                throw new StationException("stationDAO Exception: Station " + stations.StationNumber + " already exists.");
            }
            XElement stationElem = new XElement("StationDAO", 
                new XElement("StationNumber", stations.StationNumber),
                new XElement("StationName", stations.StationName),
                new XElement("Longtitude", stations.Longtitude),
                new XElement("Latitude", stations.Latitude),
                new XElement("Deleted", stations.Deleted));
            stationsRootElem.Add(stationElem);
            XMLTools.SaveListToXMLElement(stationsRootElem, stationPath);
            return true;
        }

        public bool RemoveStation(StationDAO station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
            XElement sta1 = (from sta in stationsRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == station.StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                sta1.Element("Deleted").Value = true.ToString();
                XElement stationLinesElem = XMLTools.LoadListFromXMLElement(stationLinePath);
                for (int i = 0; i < stationLinesElem.Elements().Count(); i++)
                {
                    var currentStationLine = stationLinesElem.Elements().ElementAt(i);
                    if (int.Parse(currentStationLine.Element("StationNumber").Value) == station.StationNumber)
                    {
                        RemoveStationLine(new StationLineDAO
                        {
                            StationNumber = int.Parse(currentStationLine.Element("StationNumber").Value),
                            LineNumber = int.Parse(currentStationLine.Element("LineNumber").Value),
                            NumberStationInLine = int.Parse(currentStationLine.Element("NumberStationInLine").Value),
                            Deleted = bool.Parse(currentStationLine.Element("Deleted").Value)

                        });
                    }
                }
                XMLTools.SaveListToXMLElement(stationsRootElem, stationPath);
            }
            else
            {
                throw new StationException("stationDAO Exception: The station " + station.StationNumber + " does not exists in the system.");
            }
            return true;
        }

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
            //if (station == null)
            //{
            //    return null;
            //}
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

        public bool UpdateStation(DO.StationDAO station)
        {
            XElement stationsRootElem = XMLTools.LoadListFromXMLElement(stationPath);
            XElement sta1 = (from sta in stationsRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == station.StationNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                for (int i = 0; i < stationsRootElem.Elements().Count(); i++)
                {
                    var currentStation = stationsRootElem.Elements().ElementAt(i);
                    if (int.Parse(sta1.Element("StationNumber").Value) == int.Parse(currentStation.Element("StationNumber").Value))
                    {
                        currentStation.Element("StationName").Value = station.StationName;
                        currentStation.Element("Longtitude").Value = station.Longtitude.ToString();
                        currentStation.Element("Latitude").Value = station.Latitude.ToString();
                        //currentStation.Remove();
                    }
                }
               // XElement stationElem = new XElement("StationDAO", 
               //     new XElement("StationNumber", Int32.Parse(sta1.Element("StationNumber").Value)),
               //new XElement("StationName", sta1.Element("StationName").Value),
               //new XElement("Longtitude", double.Parse(sta1.Element("Longtitude").Value)),
               //new XElement("Latitude", double.Parse(sta1.Element("Latitude").Value)),
               //new XElement("Deleted", bool.Parse(sta1.Element("Deleted").Value)));
               // stationsRootElem.Add(stationElem);
                
                XMLTools.SaveListToXMLElement(stationsRootElem, stationPath);
            }
            else
            {
                return false;
            }
            return true;
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
        //               Duration = XmlConvert.ToTimeSpanExact(p.Element("Duration").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
        //           }
        //           where predicate(p1)
        //           select p1;
        //}

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
                       AverageTravelTime = XmlConvert.ToTimeSpan(coupleSta.Element("AverageTravelTime").Value),
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
                                                       AverageTravelTime = XmlConvert.ToTimeSpan(coupleSta.Element("AverageTravelTime").Value)
                                                   }).FirstOrDefault();

            //if (coupleStation == null)
            //{
            //    return null;
            //}
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
                return false;
            }
            XElement coupleStationElem = new XElement("CoupleStationInRowDAO", 
                new XElement("StationNumberOne", coupleStation.StationNumberOne),
                new XElement("StationNumberTwo", coupleStation.StationNumberTwo),
                new XElement("Distance",coupleStation.Distance),
                new XElement("AverageTravelTime", coupleStation.AverageTravelTime));
            coupleStationInRowRootElem.Add(coupleStationElem);
            XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, coupleStationInRowPath);
            return true;
        }

        public bool RemoveCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow)
        {
            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
            XElement coupleStation = (from coupleSta in coupleStationInRowRootElem.Elements()
                                       where int.Parse(coupleSta.Element("StationNumberOne").Value) == coupleStationInRow.StationNumberOne
                                       && int.Parse(coupleSta.Element("StationNumberTwo").Value) == coupleStationInRow.StationNumberTwo
                                       select coupleSta).FirstOrDefault();

            if (coupleStation != null)
            {
                coupleStation.Remove();
                XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, coupleStationInRowPath);
                return true;
            }
            return false;
        }

        public IEnumerable<CoupleStationInRowDAO> GetCoupleStationInRowDAOInBusLine(BusLineDAO busLine)
        {
            var stationLines = GetAllStationsLineOfBusLine(busLine.LineNumber);
            IEnumerable<CoupleStationInRowDAO> list = new List<CoupleStationInRowDAO>();
            foreach (var item in GetAllCoupleStationInRow())
            {
                for (int i = 0; i < stationLines.Count() - 1; i++)
                {
                    if (stationLines.ToList()[i].StationNumber == item.StationNumberOne
                        && stationLines.ToList()[i + 1].StationNumber == item.StationNumberTwo)
                    {
                        list.ToList().Add(item);
                    }
                }
            }
            return list;
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
                for (int i = 0; i < coupleStationInRowRootElem.Elements().Count(); i++)
                {
                    var currentCoupleStation = coupleStationInRowRootElem.Elements().ElementAt(i);
                    if (int.Parse(currentCoupleStation.Element("StationNumberOne").Value) == coupleStation.StationNumberOne &&
                        int.Parse(currentCoupleStation.Element("StationNumberTwo").Value) == coupleStation.StationNumberTwo)
                    {
                        currentCoupleStation.Remove();
                    }
                }
                XElement coupleStationElem = new XElement("CoupleStationInRowDAO",
                    new XElement("StationNumberOne", Int32.Parse(coupleStation1.Element("StationNumberOne").Value)),
                    new XElement("StationNumberTwo", coupleStation1.Element("StationNumberTwo").Value),
                    new XElement("Distance", Int32.Parse(coupleStation1.Element("Distance").Value)),
                    new XElement("AverageTravelTime", XmlConvert.ToTimeSpan(coupleStation1.Element("AverageTravelTime").Value)));
                
                coupleStationInRowRootElem.Add(coupleStationElem);

                XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, coupleStationInRowPath);
                return true;
            }
            else
            {
                throw new CoupleStationException("coupleStationDAO Exception: The couple stations: " + coupleStation.StationNumberOne + ',' + coupleStation.StationNumberTwo + " does not exists in the system.");
            }
        }
        #endregion


        #region Station Line
        public bool AddStationLine(DO.StationLineDAO stationLine)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            XElement sta1 = (from st in stationLineRootElem.Elements()
                             where int.Parse(st.Element("StationNumber").Value) == stationLine.StationNumber
                             && int.Parse(st.Element("LineNumber").Value) == stationLine.LineNumber
                             && !bool.Parse(st.Element("Deleted").Value)
                             select st).FirstOrDefault();
            if (sta1 != null)
            {
                throw new StationLineException("stationLineDAO Exception: The station line " + stationLine.StationNumber + " already exists in the system.");
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
            stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);

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
                             && int.Parse(sta.Element("LineNumber").Value) == stationLine.LineNumber
                             && !bool.Parse(sta.Element("Deleted").Value)
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                sta1.Element("Deleted").Value = true.ToString();
                XMLTools.SaveListToXMLElement(stationLineRootElem, stationLinePath);
                return true;
                //foreach (var currentStationLine in stationLineRootElem.Elements())
                //{
                //    if (int.Parse(currentStationLine.Element("StationNumber").Value) == stationLine.StationNumber
                //         && int.Parse(currentStationLine.Element("LineNumber").Value) == stationLine.LineNumber)
                //    {
                //        currentStationLine.Element("Deleted").Value = true.ToString();
                //        XMLTools.SaveListToXMLElement(stationLineRootElem, stationLinePath);
                //        stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);

                //        IEnumerable<StationLineDAO> list = new List<StationLineDAO>(GetAllStationsLineOfBusLine(stationLine.LineNumber));
                //        for (int i = list.Count() - 1; i >= stationLine.NumberStationInLine; i--)
                //        {
                //            list.ToList()[i].NumberStationInLine = i;
                //        }
                //        if (stationLine.NumberStationInLine > 0 && stationLine.NumberStationInLine < list.Count())
                //        {
                //            CoupleStationInRowDAO coupleStation1 = GetOneCoupleStationInRow(list.ToList()[stationLine.NumberStationInLine - 1].StationNumber, stationLine.StationNumber);
                //            CoupleStationInRowDAO coupleStation2 = GetOneCoupleStationInRow(stationLine.StationNumber, list.ToList()[stationLine.NumberStationInLine].StationNumber);
                //            CoupleStationInRowDAO coupleStation = new CoupleStationInRowDAO
                //            {
                //                StationNumberOne = coupleStation1.StationNumberOne,
                //                StationNumberTwo = coupleStation2.StationNumberTwo,
                //                Distance = coupleStation1.Distance + coupleStation2.Distance,
                //                AverageTravelTime = coupleStation1.AverageTravelTime + coupleStation2.AverageTravelTime
                //            };
                //            XElement coupleStationElem = new XElement("CoupleStationInRowDAO", 
                //                new XElement("StationNumberOne", Int32.Parse(coupleStation.StationNumberOne.ToString())),
                //                new XElement("StationNumberTwo", Int32.Parse(coupleStation.StationNumberTwo.ToString())),
                //                 new XElement("Distance", Int32.Parse(coupleStation.Distance.ToString())),
                //                 new XElement("AverageTravelTime", coupleStation.AverageTravelTime));


                //            XElement coupleStationInRowRootElem = XMLTools.LoadListFromXMLElement(coupleStationInRowPath);
                //            XElement newCoupleStation = (from sta in coupleStationInRowRootElem.Elements()
                //                                         where int.Parse(sta.Element("StationNumberOne").Value) == coupleStation.StationNumberOne
                //                                         && int.Parse(sta.Element("StationNumberTwo").Value) == coupleStation.StationNumberTwo
                //                                         select sta).FirstOrDefault();
                //            if (newCoupleStation == null)
                //            {
                //                coupleStationInRowRootElem.Add(coupleStationElem);
                //                XMLTools.SaveListToXMLElement(coupleStationInRowRootElem, coupleStationInRowPath);
                //            }
                           
                //        }
                //        foreach (var item in list)
                //        {
                //            UpdateStationLine(item);
                //        }
                //        return true;
                //    }
                //}
            }
            throw new StationLineException("stationLineDAO Exception: The station line " + stationLine.StationNumber + " does not exists in the system.");
            
        }

        public DO.StationLineDAO GetOneStationLine(int lineNumber, int stationNumber)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            StationLineDAO stationLine = (from sta in stationLineRootElem.Elements()
                                          where int.Parse(sta.Element("StationNumber").Value) == stationNumber
                                          && int.Parse(sta.Element("LineNumber").Value) == lineNumber
                                          select new StationLineDAO()
                                          {
                                              LineNumber = Int32.Parse(sta.Element("LineNumber").Value),
                                              StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                                              NumberStationInLine = Int32.Parse(sta.Element("NumberStationInLine").Value),
                                              Deleted = bool.Parse(sta.Element("Deleted").Value)
                                          }).FirstOrDefault();
            //if (stationLine == null)
            //{
            //    return null;
            //}
            return stationLine;
        }

        public IEnumerable<DO.StationLineDAO> GetAllStationsLineOfBusLine(int lineNumber)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            return from sta in stationLineRootElem.Elements()
                   where int.Parse(sta.Element("LineNumber").Value) == lineNumber
                   && !bool.Parse(sta.Element("Deleted").Value)
                   orderby int.Parse(sta.Element("NumberStationInLine").Value)
                   select new StationLineDAO()
                   {
                       LineNumber = Int32.Parse(sta.Element("LineNumber").Value),
                       StationNumber = Int32.Parse(sta.Element("StationNumber").Value),
                       NumberStationInLine = Int32.Parse(sta.Element("NumberStationInLine").Value),
                       Deleted = bool.Parse(sta.Element("Deleted").Value)
                   };
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

        public bool UpdateStationLine(DO.StationLineDAO stationLine)
        {
            XElement stationLineRootElem = XMLTools.LoadListFromXMLElement(stationLinePath);
            XElement sta1 = (from sta in stationLineRootElem.Elements()
                             where int.Parse(sta.Element("StationNumber").Value) == stationLine.StationNumber
                             && int.Parse(sta.Element("LineNumber").Value) == stationLine.LineNumber
                             select sta).FirstOrDefault();
            if (sta1 != null)
            {
                //for (int i = 0; i < stationLineRootElem.Elements().Count(); i++)
                //{
                //    var currentStationLine = stationLineRootElem.Elements().ElementAt(i);
                //    if(Int32.Parse(currentStationLine.Element("StationNumber").Value)==stationLine.StationNumber
                //        && Int32.Parse(currentStationLine.Element("LineNumber").Value) == stationLine.LineNumber)
                //    {
                        sta1.Element("NumberStationInLine").Value = stationLine.NumberStationInLine.ToString();
                        sta1.Element("Deleted").Value = stationLine.Deleted.ToString();
                        //currentStationLine.Remove();
                //    }
                //}
                //XElement stationLineElem = new XElement("StationLineDAO",
                //new XElement("LineNumber", stationLine.LineNumber),
                //new XElement("StationNumber", stationLine.StationNumber),
                //new XElement("NumberStationInLine", stationLine.NumberStationInLine),
                //new XElement("Deleted", stationLine.Deleted));
                //stationLineRootElem.Add(stationLineElem);
                XMLTools.SaveListToXMLElement(stationLineRootElem, stationLinePath);
                return true;
            }
            return false;
        }
       
        #endregion


        #region BusLine
        public bool AddBusLine(DO.BusLineDAO busLine)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            XElement busLine1 = (from busLin in busLineRootElem.Elements()
                                 where int.Parse(busLin.Element("LineNumber").Value) == busLine.LineNumber
                                 && !bool.Parse(busLin.Element("Deleted").Value)
                                 select busLin).FirstOrDefault();
            if (busLine1 != null)
            {
                throw new BusLineException("busLineDAO Exception: The bus line " + busLine.LineNumber + " already exists.");
               
            }
            XElement busLineElem = new XElement("BusLineDAO", 
                new XElement("LineNumber", busLine.LineNumber),
                new XElement("CurrentSerialNB", busLine.CurrentSerialNB),
                new XElement("FirstStationNumber", busLine.FirstStationNumber),
                new XElement("LastStationNumber", busLine.LastStationNumber),
                new XElement("Area", busLine.Area),
                new XElement("Deleted", busLine.Deleted));
            busLineElem.Element("CurrentSerialNB").Value = Configuration.SerialBusLine.ToString();

            busLineRootElem.Add(busLineElem);
            XMLTools.SaveListToXMLElement(busLineRootElem, busLinePath);
            return true;
        }

        public bool RemoveBusLine(BusLineDAO busLine)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            for (int i = 0; i < busLineRootElem.Elements().Count(); i++)
            {
                XElement currentBusLine = busLineRootElem.Elements().ElementAt(i);

                if (int.Parse(currentBusLine.Element("CurrentSerialNB").Value) == busLine.CurrentSerialNB)
                {
                    //var list = GetAllStationsLineOfBusLine(busLine.LineNumber);
                    //for (int j = 0; j < list.Count(); j++)
                    //{
                    //    var item = list.ToList()[j];
                    //    RemoveStationLine(item);
                    //}
                    currentBusLine.Element("Deleted").Value = true.ToString();

                    XMLTools.SaveListToXMLElement(busLineRootElem, busLinePath);
                    return true;
                }
            }
            throw new BusLineException("busLineDAO Exception: The bus line " + busLine.LineNumber + " does not exists in the system.");
           
        }

        public DO.BusLineDAO GetOneBusLine(int lineNumber)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            BusLineDAO busLine = (from busLin in busLineRootElem.Elements()
                                          where int.Parse(busLin.Element("LineNumber").Value) == lineNumber
                                          select new BusLineDAO()
                                          {
                                              CurrentSerialNB = Int32.Parse(busLin.Element("CurrentSerialNB").Value),
                                              LineNumber = Int32.Parse(busLin.Element("LineNumber").Value),
                                              FirstStationNumber = Int32.Parse(busLin.Element("FirstStationNumber").Value),
                                              LastStationNumber = Int32.Parse(busLin.Element("LastStationNumber").Value),
                                              Area = (Area)Enum.Parse(typeof(Area) ,busLin.Element("Area").Value),
                                              Deleted = bool.Parse(busLin.Element("Deleted").Value)
                                          }).FirstOrDefault();
            //if (busLine == null)
            //{
            //    return null;
            //}
            return busLine;
        }

        public IEnumerable<DO.BusLineDAO> GetAllBusLines()
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            return from busLin in busLineRootElem.Elements()
                   select new BusLineDAO()
                   {
                       CurrentSerialNB = Int32.Parse(busLin.Element("CurrentSerialNB").Value),
                       LineNumber = Int32.Parse(busLin.Element("LineNumber").Value),
                       FirstStationNumber = Int32.Parse(busLin.Element("FirstStationNumber").Value),
                       LastStationNumber = Int32.Parse(busLin.Element("LastStationNumber").Value),
                       Area = (Area)Enum.Parse(typeof(Area), busLin.Element("Area").Value),
                       Deleted = bool.Parse(busLin.Element("Deleted").Value)
                   };
        }

        public bool UpdateBusLine(BusLineDAO busLine)
        {
            XElement busLineRootElem = XMLTools.LoadListFromXMLElement(busLinePath);
            XElement busLine1 = (from busLin in busLineRootElem.Elements()
                                 where int.Parse(busLin.Element("CurrentSerialNB").Value) == busLine.CurrentSerialNB
                                 select busLin).FirstOrDefault();
            if(busLine1!=null)
            {
                for(int i=0; i<busLineRootElem.Elements().Count();i++)
                {
                    XElement currentBusLine = busLineRootElem.Elements().ElementAt(i);
                    if(Int32.Parse(currentBusLine.Element("currentSerialNB").Value)== Int32.Parse(busLine1.Element("currentSerialNB").Value))
                    {
                        busLine1.Remove();
                    }
                }
                XElement busLineElem = new XElement("BusLineDAO",
                    new XElement("LineNumber", Int32.Parse(busLine1.Element("LineNumber").Value)),
                    new XElement("CurrentSerialNB", Int32.Parse(busLine1.Element("CurrentSerialNB").Value)),
                    new XElement("FirstStationNumber", Int32.Parse(busLine1.Element("FirstStationNumber").Value)),
                    new XElement("LastStationNumber", Int32.Parse(busLine1.Element("LastStationNumber").Value)),
                    new XElement("Area", (Area)Enum.Parse(typeof(Area), busLine1.Element("Area").Value)),
                    new XElement("Deleted", bool.Parse(busLine1.Element("Deleted").Value)));
                busLineRootElem.Add(busLineElem);
                XMLTools.SaveListToXMLElement(busLineRootElem, busLinePath);
                return true;
            }
            return false;
        }
        #endregion


        #region BusInTravel
        public bool AddBusInTravel(DO.BusInTravelDAO busInTravel)
        {
            XElement busInTravelRootElem = XMLTools.LoadListFromXMLElement(busInTravelPath);
            XElement busInTravel1 = (from busInTra in busInTravelRootElem.Elements()
                                     where busInTra.Element("LicenseNumber").Value.ToString() == busInTravel.LicenseNumber
                                     && int.Parse(busInTra.Element("CurrentSerialNB").Value) == busInTravel.CurrentSerialNB
                                     && XmlConvert.ToTimeSpan(busInTra.Element("Start").Value) == busInTravel.Start
                                     && bool.Parse(busInTra.Element("IsActive").Value)
                                     select busInTra).FirstOrDefault();
            if (busInTravel1 != null)
            {
                //throw;
                return false;
            }
            XElement busInTravelElem = new XElement("BusInTravelDAO", new XElement("CurrentSerialNB", Int32.Parse(busInTravel1.Element("CurrentSerialNB").Value)),
                new XElement("LicenseNumber", busInTravel1.Element("LicenseNumber").Value).ToString(),
                new XElement("LineNumber", int.Parse(busInTravel1.Element("LineNumber").Value)),
                new XElement("Start", XmlConvert.ToTimeSpan(busInTravel1.Element("Start").Value)),
                new XElement("ActualStart", XmlConvert.ToTimeSpan(busInTravel1.Element("ActualStart").Value)),
               new XElement("LastStationTimePassedThrough", XmlConvert.ToTimeSpan(busInTravel1.Element("LastStationTimePassedThrough").Value)),
               new XElement("NextStationTimePassedThrough", XmlConvert.ToTimeSpan(busInTravel1.Element("NextStationTimePassedThrough").Value)),
               new XElement("LastStationNumberPassedThrough", int.Parse(busInTravel1.Element("LastStationNumberPassedThrough").Value)),
               new XElement("IsActive", bool.Parse(busInTravel1.Element("IsActive").Value)),
                new XElement("DriverID", XmlConvert.ToTimeSpan(busInTravel1.Element("DriverID").Value).ToString()));
            busInTravelRootElem.Add(busInTravelElem);
            XMLTools.SaveListToXMLElement(busInTravelRootElem, busInTravelPath);
            return true;
        }

        public bool RemoveBusInTravel(BusInTravelDAO busInTravel)
        {
            throw new NotImplementedException();
        }

        public List<BusInTravelDAO> GetAllBusesInTravel()
        {
            throw new NotImplementedException();
        }

        public bool UpdateBusInTravel(BusInTravelDAO busInTravel)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Bus
        public bool AddBus(BusDAO bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);
            XElement bus1 = (from bu in busRootElem.Elements()
                                 where bu.Element("LicenseNumber").Value == bus.LicenseNumber
                                 && !bool.Parse(bu.Element("Deleted").Value)
                                 select bu).FirstOrDefault();
            if (bus1 != null)
            {
                throw new BusException("busDAO Exception: The bus " + bus.LicenseNumber + " already exists in the system.");
            }
            XElement busElem = new XElement("BusDAO",
                 new XElement("LicenseNumber", bus.LicenseNumber),
                 new XElement("Maintnance", bus.Maintnance),
                 new XElement("StartOfWork", bus.StartOfWork),
                 new XElement("Status", bus.Status),
                 new XElement("TotalKms", bus.TotalKms),
                 new XElement("LastTreatment", bus.LastTreatment),
                 new XElement("Fuel", bus.Fuel),
                 new XElement("Deleted", bus.Deleted));
            busRootElem.Add(busElem);
            XMLTools.SaveListToXMLElement(busRootElem, busPath);
            return true;
        }

        
        public bool ChooseBusForDrive(BusDAO bus)
        {
            if (bus.Status == Status.READY_TO_DRIVE)
                return true;
            throw new BusException("BusDAO Exception: The bus " + bus.LicenseNumber + " is not ready to drive.");
        }

        public bool RefuelBus(BusDAO bus)
        {
            if ((bus.Status == Status.READY_TO_DRIVE || bus.Status == Status.NEED_REFUELING) && bus.Fuel != 1200)
            {
                return true;
            }
            throw new BusException("BusDAO Exception: The bus " + bus.LicenseNumber + " needs to be refueled.");
        }

        public bool Treatment(BusDAO bus)
        {
            if ((bus.Status == Status.READY_TO_DRIVE || bus.Status == Status.NEED_REFUELING || bus.Status == Status.NEED_TREATMENT) && bus.Maintnance != 0)
                return true;
            throw new BusException("BusDAO Exception: The bus " + bus.LicenseNumber + " needs to be treated.");
        }

        [Obsolete]
        public IEnumerable<BusDAO> GetAllBusses()
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);
            return from currentBus in busRootElem.Elements()
                   where !bool.Parse(currentBus.Element("Deleted").Value)
                   select new BusDAO()
                   {
                       Fuel = Int32.Parse(currentBus.Element("Fuel").Value),
                       LastTreatment = XmlConvert.ToDateTime(currentBus.Element("LastTreatment").Value),
                       LicenseNumber = currentBus.Element("LicenseNumber").Value,
                       Maintnance = Int32.Parse(currentBus.Element("Maintnance").Value),
                       Status = (Status)Enum.Parse(typeof(Status), currentBus.Element("Status").Value),
                       Deleted = bool.Parse(currentBus.Element("Deleted").Value),
                       StartOfWork = XmlConvert.ToDateTime(currentBus.Element("StartOfWork").Value),
                       TotalKms = Int32.Parse(currentBus.Element("TotalKms").Value)
                   };
        }
        public bool RemoveBus(BusDAO bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);
            XElement bus1 = (from bu in busRootElem.Elements()
                             where bu.Element("LicenseNumber").Value == bus.LicenseNumber
                             && !bool.Parse(bu.Element("Deleted").Value)
                             select bu).FirstOrDefault();
            if (bus1 == null)
            {
                throw new BusException("busDAO Exception: The bus " + bus.LicenseNumber + " does not exists in the system.");
            }
            bus1.Element("Deleted").Value = true.ToString();
            XMLTools.SaveListToXMLElement(busRootElem, busPath);
            return true;
        }

        [Obsolete]
        public BusDAO GetOneBus(string license)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);
            return (from bus1 in busRootElem.Elements()
                             where bus1.Element("LicenseNumber").Value == license
                             && !bool.Parse(bus1.Element("Deleted").Value)
                             select new BusDAO()
                             {
                                 Fuel = Int32.Parse(bus1.Element("Fuel").Value),
                                 LastTreatment = XmlConvert.ToDateTime(bus1.Element("LastTreatment").Value),
                                 LicenseNumber = bus1.Element("LicenseNumber").Value,
                                 Maintnance = Int32.Parse(bus1.Element("Maintnance").Value),
                                 Status = (Status)Enum.Parse(typeof(Status), bus1.Element("Status").Value),
                                 Deleted = bool.Parse(bus1.Element("Deleted").Value),
                                 StartOfWork = XmlConvert.ToDateTime(bus1.Element("StartOfWork").Value),
                                 TotalKms = Int32.Parse(bus1.Element("TotalKms").Value)
                             }).FirstOrDefault();
        }

        [Obsolete]
        public bool UpdateBus(BusDAO bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);
            XElement bus1 = (from bu in busRootElem.Elements()
                             where bu.Element("LicenseNumber").Value == bus.LicenseNumber
                             && !bool.Parse(bu.Element("Deleted").Value)
                             select bu).FirstOrDefault();
            if (bus1 == null)
            {
                throw new BusException("busDAO Exception: The bus " + bus.LicenseNumber + " does not exist in the system.");

            }
            bus1.Element("Fuel").Value = bus.Fuel.ToString();
            bus1.Element("LastTreatment").Value = XmlConvert.ToString(bus.LastTreatment);
            bus1.Element("Maintnance").Value = bus.Maintnance.ToString();
            bus1.Element("Status").Value = bus.Status.ToString();
            bus1.Element("TotalKms").Value = bus.TotalKms.ToString();
            XMLTools.SaveListToXMLElement(busRootElem, busPath);
            return true;
        }

        #endregion
        public IEnumerable<BusLineDAO> GetBusLineInStation(int stationNmber)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<BusLineDAO> GetAllBusLines()
        //{
        //    throw new NotImplementedException();
        //}

        public bool AddLineInService(LineInServiceDAO lineInService)
        {
            throw new NotImplementedException();
        }

        public bool RemoveLineInService(LineInServiceDAO lineInService)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLineInService(LineInServiceDAO lineInService)
        {
            throw new NotImplementedException();
        }


    }









}


