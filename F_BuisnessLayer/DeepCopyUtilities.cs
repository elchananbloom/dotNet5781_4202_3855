using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public static class DeepCopyUtilities
    {
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
        public static DO.BusLineDAO CopyToNewBusLine(this BO.BusLineBO busLineBO)
        {
            DO.BusLineDAO result = (DO.BusLineDAO)busLineBO.CopyPropertiesToNew(typeof(DO.BusLineDAO));
            // propertys' names changed? copy them here...
            result.FirstStationNumber = busLineBO.StationLines.ElementAt(0).Station.StationNumber;
            result.LastStationNumber = busLineBO.StationLines.ElementAt(busLineBO.StationLines.Count()-1).Station.StationNumber;
            return result;
        }
    }
}
