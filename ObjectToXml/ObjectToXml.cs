using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource;
using DAL;
using DALAPI;
using DO;
using System.Xml.Linq;
using System.Xml;

namespace ObjectToXml
{
    class ObjectToXml
    {
        static void Main(string[] args)
        {
            XMLTools.SaveListToXMLSerializer<BusLineDAO>(DataSource.DataSource.BusLinesList, @"BusLines.xml");
            XMLTools.SaveListToXMLSerializer<BusDAO>(DataSource.DataSource.BussesList, @"Busses.xml");
           // XMLTools.SaveListToXMLSerializer<CoupleStationInRowDAO>(DataSource.DataSource.CoupleStationInRowList, @"CoupleStationInRow.xml");
            XMLTools.SaveListToXMLSerializer<StationLineDAO>(DataSource.DataSource.StationLinesList, @"StationLines.xml");
            XMLTools.SaveListToXMLSerializer<StationDAO>(DataSource.DataSource.StationsList, @"Stations.xml");
            XElement myElement = new XElement("CoupleStationInRowList");
            foreach (var item in DataSource.DataSource.CoupleStationInRowList)
            {
                XElement coupleStation = new XElement("CoupleStationInRow", new XElement("StationNumberOne", item.StationNumberOne),
                                   new XElement("StationNumberTwo", item.StationNumberTwo),
                                   new XElement("Distance", item.Distance),
                                   new XElement("AverageTravelTime", XmlConvert.ToString(item.AverageTravelTime)));
                myElement.Add(coupleStation);
            }
            //XmlConvert.ToTimeSpan()
            XMLTools.SaveListToXMLElement(myElement, @"CoupleStationInRow.xml");
            //XMLTools.SaveListToXMLElement()
        }
    }
}
