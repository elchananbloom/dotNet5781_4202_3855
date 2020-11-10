using System.Collections;
using System.Collections.Generic;
using System;
namespace dotNet_02_4202_3855
{
    class BusLinesCollection : IEnumerable
    {
        List<BusLine> busLinesCollection = new List<BusLine>();
        //this func adds bus line to the bus lines collection
        public void addBusLine(BusLine bus)
        {
            int count = countBusLinesInCollection(bus);
            if (count >= 2)
            {
                throw new ArgumentException("There are already more than two bus lines with the same line number.");
                // Console.WriteLine("There are already more than two bus lines with the same line number.");
            }
            busLinesCollection.Add(bus);
        }
        //this func delete bus line from the bus line collection.
        public void subBusLine(BusLine bus, BusLineStation firstStation)
        {
            int count = countBusLinesInCollection(bus);
            if (count > 0)
            {
                for (int index = 0; index < busLinesCollection.Count; index++)
                {
                    if (bus.BusLineNumber == busLinesCollection[index].BusLineNumber && bus.FirstStation == firstStation)
                    {
                        busLinesCollection.RemoveAt(index);
                    }
                }
            }

            else
            {
                throw new KeyNotFoundException("This bus line does not exists in the system");
                //Console.WriteLine("This bus line does not exists in the system");
            }
        }

        //this func returns bus lines list that pass through this bus station
        public List<BusLine> busLinesInStation(string number)
        {
            List<BusLine> newBusLine = new List<BusLine>();
            for (int index = 0; index < busLinesCollection.Count; index++)
            {
                if (busLinesCollection[index].stationCheck(number, busLinesCollection[index].stationListHaloch))
                {
                    newBusLine.Add(busLinesCollection[index]);
                }
            }
            if (newBusLine.Count == 0)
            {
                throw new ArgumentException("There are no line buses passing through this station.");
            }
            return newBusLine;
        }
        //this func sorts by travel time the bus lines collection
        public List<BusLine> sortBusLineTimes()
        {
            List<BusLine> sortedBusLineCollection = new List<BusLine>(busLinesCollection);
            sortedBusLineCollection.Sort();
            return sortedBusLineCollection;
        }

        //this func counts how many times the bus line appears in the bus line collection.
        private int countBusLinesInCollection(BusLine bus)
        {
            int count = 0;
            foreach (BusLine buss in busLinesCollection)
            {
                if (buss == bus)
                { count++; }
            }
            return count;
        }







        //indexer
        public BusLine this[int busLineNumber,BusLineStation first]
        {
            get
            {
                BusLine result = busLinesCollection.Find(bus => bus.BusLineNumber == busLineNumber && bus.FirstStation == first);
                if (result == null)
                    throw new KeyNotFoundException();
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