using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using G_DALAPI;
namespace F_BuisnessLayer
{
    class BLimp:IBL
    {
        IDal dal= 
        bool AddBus(BusBO bus)
        {
            if()
            if (DataSource.BussesList.Exists(currentBus => currentBus.LicenseNumber == bus.LicenseNumber))
            {
                throw new BusException("License exists allready.");
                //return false;
            }
            BusDAO cloned = bus.Cloned();

            
            DataSource.BussesList.Add(cloned);
            return true;
        }
    }
}
