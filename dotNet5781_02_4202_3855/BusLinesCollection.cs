using System.Collections;
using System.Collections.Generic;
using System;
namespace dotNet_02_4202_3855
{
    class BusLinesCollection : IEnumerable
    {
        public List<BusLine> busLinesCollection = new List<BusLine>();
        List<BusLineStation> allBusStations = new List<BusLineStation>();

        //prints all the stations and the lines passing through them.
        public void PrintAllStationsAndLinesInStation()
        {
            foreach (BusLine line in busLinesCollection)
            {
                foreach(BusLineStation station in line.stationList)
                {
                    if(!line.StationChecking(station,allBusStations))
                    {
                        allBusStations.Add(station);
                    }
                }
            }
            Console.WriteLine("Printing all the stations and the bus lines passing through them.\nStations:");
            foreach(BusLineStation station in allBusStations)
            {
                Console.WriteLine(station+"\n\t\tBus lines numbers:");
                foreach(BusLine bus in BusLinesInStation(station.BusStationKey))
                {
                    Console.WriteLine("\t\t"+bus.BusLineNumber);
                }
            }
        }
        //this func adds bus line to the bus lines collection
        public void AddBusLine(BusLine bus)
        {
            int count = CountBusLinesInCollection(bus);
            if (count >= 2)
            {
                throw new ArgumentException("There are already more than two bus lines with the same line number.");
            }
            busLinesCollection.Add(bus);
        }
        //this func delete bus line from the bus line collection.
        public void SubBusLine(BusLine bus, BusLineStation firstStation)
        {
            int count = CountBusLinesInCollection(bus);
            if (count > 0)
            {
                for (int index = 0; index < busLinesCollection.Count; index++)
                {
                    if (bus.BusLineNumber == busLinesCollection[index].BusLineNumber && bus.FirstStation == firstStation)
                    {
                        busLinesCollection.RemoveAt(index);
                        Console.WriteLine("The bus line has been removed successfully.");
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("This bus line does not exists in the system");
            }
        }
        //this func returns bus lines list that pass through this bus station
        public List<BusLine> BusLinesInStation(int stationNmber)
        {
            List<BusLine> newBusLine = new List<BusLine>();
            for (int index = 0; index < busLinesCollection.Count; index++)
            {
                if (busLinesCollection[index].StationCheck(stationNmber, busLinesCollection[index].stationList))
                {
                    newBusLine.Add(busLinesCollection[index]);
                }
            }
            if (newBusLine.Count == 0)
            {
                throw new KeyNotFoundException("The station does not exists.");

                //throw new ArgumentException("There are no line buses passing through this station.");
            }
            return newBusLine;
        }
        //this func sorts by travel time the bus lines collection
        public List<BusLine> SortBusLineTimes()
        {
            List<BusLine> sortedBusLineCollection = new List<BusLine>(busLinesCollection);
            sortedBusLineCollection.Sort();
            return sortedBusLineCollection;
        }
        //this func counts how many times the bus line appears in the bus line collection.
        private int CountBusLinesInCollection(BusLine bus)
        {
            int count = 0;
            foreach (BusLine buss in busLinesCollection)
            {
                if (buss == bus)
                { count++; }
            }
            return count;
        }
        //indexer: returns bus line in the bus line collection by his line number and his first station.
        public BusLine this[int busLineNumber,BusLineStation first]
        {
            get
            {
                BusLine result = busLinesCollection.Find(bus => bus.BusLineNumber == busLineNumber && bus.FirstStation.BusStationKey == first.BusStationKey);
                if (result == null)
                    throw new KeyNotFoundException("This bus line does not exists in the system");
                return result;
            }
        }
        //IEnumerator
        public IEnumerator GetEnumerator()
        {
            return busLinesCollection.GetEnumerator();
        }
    }
}