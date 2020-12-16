using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace H_DataAccessLayer
{
    /// <summary>
    /// doesn't work with nested classes. It meshachpel the objects that comes from the BL for saving or updating them before saving them in the DS.
    /// </summary>
    static class Clone
    {

        static T Cloned<T>(this T original)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo item in original.GetType().GetProperties())
            {
                item.SetValue(result, item.GetValue(original, null));
            }
            return result;
        }

        public static BusDAO Clone1(this BusDAO source)
        {
            return new BusDAO
            {
                LicenseNumber = source.LicenseNumber,
                StartOfWork = source.StartOfWork,
                TotalKms = source.TotalKms,
                Fuel = source.Fuel,
                Status = source.Status
            };
        }

        public static BusInTravelDAO Clone2(this BusInTravelDAO source)
        {
            return new BusInTravelDAO
            {
                LicenseNumber = source.LicenseNumber,
                Line = source.Line,
                Start = source.Start
            };
        }



    }

}
