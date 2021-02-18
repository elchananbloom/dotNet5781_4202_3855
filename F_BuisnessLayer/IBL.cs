using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BuisnessLayer
{
    public interface IBL
    {
        #region BusBo
        bool AddBus(BusBO bus);
        bool RemoveBus(BusBO bus);
        BusBO GetBusBO(string licenseNumber);
        IEnumerable<BusBO> GetAllBusesBO();
        bool UpdateBus(BusBO bus);
        #endregion


        #region BusLineBo
        /// <summary>
        /// Convert DO bus line to BO 
        /// </summary>
        /// <param name="busLineDAO"></param>
        /// <returns></returns>
        BusLineBO busLineDoBoAdapter(DO.BusLineDAO busLineDAO);

        /// <summary>
        /// Add bus line
        /// </summary>
        /// <param name="busLine"></param>
        /// <param name="firstStationLineBO"></param>
        /// <param name="lastStationLineBO"></param>
        /// <returns></returns>
        /// 
        bool AddBusLine(BusLineBO busLine, StationLineBO firstStationLineBO, StationLineBO lastStationLineBO);
        /// <summary>
        /// Remove bus line
        /// </summary>
        /// <param name="busLine"></param>
        /// <returns></returns>
        bool RemoveBusLine(BusLineBO busLine);
        /// <summary>
        /// Get one bus line and all of his station lines.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        BusLineBO GetOneBusLine(int lineNumber);
        /// <summary>
        /// Get all bus lines with their station lines.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusLineBO> GetAllBusLines();
        /// <summary>
        /// Update bus line by his first and last stations
        /// </summary>
        /// <param name="busLine"></param>
        /// <param name="firstStationLineBO"></param>
        /// <param name="lastStationLineBO"></param>
        /// <returns></returns>
        bool UpdateBusLine(BusLineBO busLine, StationLineBO firstStationLineBO, StationLineBO lastStationLineBO);
        #endregion


        #region StationLineBo
        /// <summary>
        /// Convert station line from DO to BO
        /// </summary>
        /// <param name="stationLineDAO"></param>
        /// <returns></returns>
        StationLineBO StationLineDoBoAdapter(DO.StationLineDAO stationLineDAO);
        /// <summary>
        /// Add station line and add the couple stations according to the new station line.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        bool AddStationLine(StationLineBO stationLine);
        /// <summary>
        /// Remove station line and add the couple stations according to 
        /// the station line before and after the deleted station line.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        bool RemoveStationLine(StationLineBO stationLine);
        /// <summary>
        /// Update station line and update the couple stations according to the updated station line.
        /// </summary>
        /// <param name="stationLine"></param>
        /// <returns></returns>
        bool UpdateStationLine(StationLineBO stationLine);
        /// <summary>
        /// Get all station lines.
        /// </summary>
        /// <returns></returns>
        IEnumerable<StationLineBO> GetAllStationLines();
        /// <summary>
        /// Get all the bus line's stations according to all the couple station in it.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        IEnumerable<StationLineBO> GetAllStationLinesOfBusLine(int num);
        #endregion


        #region StationBO
        /// <summary>
        /// Convert DO station to BO
        /// </summary>
        /// <param name="stationDAO"></param>
        /// <returns></returns>
        StationBO StationDoBoAdapter(DO.StationDAO stationDAO);
        /// <summary>
        /// Add station.
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool AddStation(StationBO station);
        /// <summary>
        /// Remove station and updating the station line of the bus line that passes through 
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool RemoveStation(StationBO station);
        /// <summary>
        /// Update station
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool UpdateStation(StationBO station);
        /// <summary>
        /// Get one station.
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        StationBO GetOneStation(int stationNumber);
        /// <summary>
        /// Get all the bus lines that passes through the station.
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        IEnumerable<BusLineBO> GetAllBusLinesInStation(int stationNumber);
        /// <summary>
        /// Get all stations.
        /// </summary>
        /// <returns></returns>
        IEnumerable<StationBO> GetAllStations();
        #endregion


    }
}
