using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_BuisnessLayer
{
    public static class BLFactory
    {
        public static IBL BlInstance
        {
            get => BLimp.getInstance();
        }
    }
}
