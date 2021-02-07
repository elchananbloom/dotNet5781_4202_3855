using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource;
using DAL;
using DALAPI;
using DO;

namespace ObjectToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLTools.SaveListToXMLSerializer<BusLineDAO>(DataSource.DataSource.BusLinesList, @"BusLines.xml");
        }
    }
}
