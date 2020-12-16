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
        //Eliezer
        bool addBus(BusDAO bus);
        bool update(BusDAO bus);
        void delete(BusDAO bus);
        BusDAO read(string license);
        List<BusDAO> getBusses();
        bool addBusInTravel(BusInTravelDAO bus);
        List<BusInTravelDAO> getBusesTravel();
        //eliezer


    }
}
