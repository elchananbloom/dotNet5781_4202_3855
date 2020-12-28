using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace F_BuisnessLayer
{
    interface IBL
    {
        bool AddBus(BusBO bus);
        bool RemoveBus(BusBO bus);
        BusBO GetBusBO(string licenseNumber);
        IEnumerable<BusBO> GetAllBusesBO();
        bool UpdateBus(BusBO bus);
            
    }
}
