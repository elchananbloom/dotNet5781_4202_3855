using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace G_DALAPI
{
    public interface IDal
    {
        #region BusDAO functions
        bool AddBus(BusDAO bus);
        bool ChooseBusForDrive(BusDAO bus);
        bool RefuelBus(BusDAO bus);
        bool Treatment(BusDAO bus);
        IEnumerable<BusDAO> GetAllBusses();
        bool RemoveBus(BusDAO bus);
        BusDAO GetOneBus(string license);
        public bool ActivateBus(string license);
        bool UpdateBus(BusDAO bus);
        #endregion


        #region BusInTravelDAO function
        bool AddBusInTravel(BusInTravelDAO busInTravel);
        bool RemoveBusInTravel(BusInTravelDAO busInTravel);
        List<BusInTravelDAO> GetAllBusesInTravel();
        List<StationLineDAO> GetAllStationsLineOfBusLine(int LineNumber);
        bool UpdateBusInTravel(BusInTravelDAO busInTravel);
        #endregion


        #region StationLineDAO functions
        bool AddStationLine(StationLineDAO stationLine);
        bool RemoveStationLine(StationLineDAO stationLine);
        bool ActivateStationLine(int lineNumber, int stationNumber);
        StationLineDAO GetOneStationLine(int lineNumber, int stationNumber);
        bool UpdateStationLine(StationLineDAO stationLine);
        #endregion


        #region StationDAO functions
        bool AddStation(StationDAO station);
        bool RemoveStation(StationDAO station);
        StationDAO GetOneStation(int stationNumber);
        IEnumerable<StationDAO> GetAllStations();
        List<BusLineDAO> GetAllBusLinesInStation(int stationNmber);
        bool UpdateStation(StationDAO station);
        #endregion

        #region BusLineDAO functions
        bool AddBusLine(BusLineDAO busLine);
        bool RemoveBusLine(BusLineDAO busLine);
        BusLineDAO GetOneBusLine(int lineNumber);
        IEnumerable<BusLineDAO> GetAllBusLines();
        bool UpdateBusLine(BusLineDAO busLine);
        #endregion

        #region CoupleStationInRowDAO functions
        List<CoupleStationInRowDAO> GetAllCoupleStationInRow();
        bool AddCoupleStationInRow(int stationNumberOne, int stationNumberTwo, TimeSpan time, int distance);
        #endregion
    }
}
