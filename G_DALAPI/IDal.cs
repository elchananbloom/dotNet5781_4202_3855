using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DALAPI
{
    public interface IDal
    {
        #region BusDAO functions
        /// <summary>
        /// Adds a new bus to the busses list.
        /// </summary>
        /// <param name="bus">Bus from DO</param>
        /// <returns></returns>
        bool AddBus(BusDAO bus);

        bool ChooseBusForDrive(BusDAO bus);
        bool RefuelBus(BusDAO bus);
        bool Treatment(BusDAO bus);
        IEnumerable<BusDAO> GetAllBusses();
        bool RemoveBus(BusDAO bus);
        BusDAO GetOneBus(string license);
        //bool ActivateBus(string license);
        bool UpdateBus(BusDAO bus);
        #endregion


        #region BusInTravelDAO function
        /// <summary>
        /// Add bus in travel to the bussesInTravel list
        /// </summary>
        /// <param name="bus">Bus from BusInTravelDAO</param>
        /// <returns></returns>
        bool AddBusInTravel(BusInTravelDAO busInTravel);

        /// <summary>
        /// Remove bus in travel from the bussesInTravel list
        /// </summary>
        /// <param name="bus">Bus from BusInTravelDAO</param>
        /// <returns></returns>
        bool RemoveBusInTravel(BusInTravelDAO busInTravel);
        /// <summary>
        /// Gets all busses in the bussesInTravel list.
        /// </summary>
        /// <returns></returns>
        List<BusInTravelDAO> GetAllBusesInTravel();
        /// <summary>
        /// Update bus in travel.
        /// </summary>
        /// <param name="busInTravel"></param>
        /// <returns></returns>
        bool UpdateBusInTravel(BusInTravelDAO busInTravel);
        #endregion


        #region StationLineDAO functions
        /// <summary>
        /// Add station line to the station lines list.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        bool AddStationLine(StationLineDAO stationLine);
        /// <summary>
        /// Remove station line by updating his delete field.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        bool RemoveStationLine(StationLineDAO stationLine);
        /// <summary>
        /// Gets a station line by his line number and his station number.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        StationLineDAO GetOneStationLine(int lineNumber, int stationNumber);
        /// <summary>
        /// Gets all the station lines inside the bus line
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns>all the stations in the bus line</returns>
        IEnumerable<StationLineDAO> GetAllStationsLineOfBusLine(int LineNumber);
        /// <summary>
        /// Gets all the station lines
        /// </summary>
        /// <returns>all the stations</returns>
        IEnumerable<StationLineDAO> GetAllStationLines();
        /// <summary>
        /// Update station line
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        bool UpdateStationLine(StationLineDAO stationLine);
        #endregion


        #region StationDAO functions
        /// <summary>
        /// Adds a new station to the stations list.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool AddStation(StationDAO station);
        /// <summary>
        /// Remove a station from the stations list.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool RemoveStation(StationDAO station);
        /// <summary>
        /// Gets one station from the stations list
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        StationDAO GetOneStation(int stationNumber);
        /// <summary>
        /// Gets all stations from the stations list
        /// </summary>
        /// <returns></returns>
        IEnumerable<StationDAO> GetAllStations();
        /// <summary>
        /// Gets all the bus lines in the station
        /// </summary>
        /// <param name="stationNmber"></param>
        /// <returns></returns>
        IEnumerable<BusLineDAO> GetBusLineInStation(int stationNmber);
        /// <summary>
        /// Update a station.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool UpdateStation(StationDAO station);
        #endregion


        #region BusLineDAO functions
        /// <summary>
        /// Adds a new bus line to the bus lines list.
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        bool AddBusLine(BusLineDAO busLine);
        /// <summary>
        /// Remove a bus line from the bus lines list.
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        bool RemoveBusLine(BusLineDAO busLine);
        /// <summary>
        /// Gets bus line from the bus line list.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        BusLineDAO GetOneBusLine(int lineNumber);
        /// <summary>
        ///  Gets all bus lines from the bus line list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusLineDAO> GetAllBusLines();
        /// <summary>
        /// Update bus line
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        bool UpdateBusLine(BusLineDAO busLine);
        #endregion


        #region CoupleStationInRowDAO functions
        /// <summary>
        /// Gets list of all the couple stations in a row from the couple station list
        /// </summary>
        /// <returns></returns>
        IEnumerable<CoupleStationInRowDAO> GetAllCoupleStationInRow();
        /// <summary>
        /// Gets list of all the couple stations in a row in one bus line from the couple station list.
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        IEnumerable<CoupleStationInRowDAO> GetCoupleStationInRowDAOInBusLine(BusLineDAO busLine);
        //void  GetCoupleStationInRowOfOneBusLine(BusLineDAO busLine);
        /// <summary>
        /// Update couple station in a row in the couple station list
        /// </summary>
        /// <param name="coupleStationInRow"></param>
        /// <returns></returns>
        bool UpdateCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow);
        /// <summary>
        /// Remove couple station in a row from the couple station list
        /// </summary>
        /// <param name="coupleStationInRow"></param>
        /// <returns></returns>
        bool RemoveCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow);
        /// <summary>
        /// Add couple station in a row to the couple station list.
        /// </summary>
        /// <param name="coupleStationInRow"></param>
        /// <returns></returns>
        bool AddCoupleStationInRow(CoupleStationInRowDAO coupleStationInRow);
        /// <summary>
        /// Gets one couple station in a row from the couple station list.
        /// </summary>
        /// <param name="stationNumberOne"></param>
        /// <param name="stationNumberTwo"></param>
        /// <returns></returns>
        CoupleStationInRowDAO GetOneCoupleStationInRow(int stationNumberOne, int stationNumberTwo);
        #endregion


        #region LineInServiceDAO functions
        /// <summary>
        /// Adds a new line in service to the line in service list.
        /// </summary>
        /// <param name="lineInService"></param>
        /// <returns></returns>
        bool AddLineInService(LineInServiceDAO lineInService);
        /// <summary>
        /// Removes a line in service from the line in service list.
        /// </summary>
        /// <param name="lineInService"></param>
        /// <returns></returns>
        bool RemoveLineInService(LineInServiceDAO lineInService);
        /// <summary>
        /// Update a line in service in the line in service list.
        /// </summary>
        /// <param name="lineInService"></param>
        /// <returns></returns>
        bool UpdateLineInService(LineInServiceDAO lineInService);
        #endregion
    }
}
