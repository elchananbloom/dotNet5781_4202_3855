using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace F_BuisnessLayer
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
        bool AddBusLine(BusLineBO busLine);
        bool RemoveBusLine(BusLineBO busLine);
        BusLineBO GetOneBusLine(int lineNumber);
        IEnumerable<BusLineBO> GetAllBusLines();
        bool UpdateBusLine(BusLineBO busLine);
        #endregion


        #region StationLineBo
        StationLineBO StationLineDoBoAdapter(DO.StationLineDAO stationLineDAO);
        bool AddStationLine(StationLineBO stationLine);
        bool RemoveStationLine(StationLineBO stationLine);
        bool UpdateStationLine(StationLineBO stationLine);
        IEnumerable<StationLineBO> GetAllStationLines(int num);
        #endregion


        #region StationBO
        StationBO StationDoBoAdapter(DO.StationDAO stationDAO);
        bool AddStation(StationBO station);
        bool RemoveStation(StationBO station);
        bool UpdateStation(StationBO station);
        StationBO GetOneStation(int stationNumber);
        IEnumerable<BusLineBO> GetAllBusLinesInStation(int stationNumber);
        IEnumerable<StationBO> GetAllStations();
        #endregion


    }
}
