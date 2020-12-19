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
    static class Cloning
    {

        public static T Cloned<T>(this T original)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo item in original.GetType().GetProperties())
            {
                item.SetValue(result, item.GetValue(original, null));
            }
            return result;
        }

        //public static BusDAO Clone(this BusDAO source)
        //{
        //    return new BusDAO
        //    {
        //        LicenseNumber = source.LicenseNumber,
        //        StartOfWork = source.StartOfWork,
        //        TotalKms = source.TotalKms,
        //        Fuel = source.Fuel,
        //        Maintnance = source.Maintnance,
        //        Status = source.Status,
        //        Deleted = source.Deleted
        //    };
        //}

        //public static BusInTravelDAO Clone(this BusInTravelDAO source)
        //{
        //    return new BusInTravelDAO
        //    {
        //        CurrentSerialNB=source.CurrentSerialNB,
        //        LicenseNumber = source.LicenseNumber,
        //        LineNumber = source.LineNumber,
        //        Start = source.Start,
        //        ActualStart = source.ActualStart,
        //        LastStationNumberPassedThrough = source.LastStationNumberPassedThrough,
        //        LastStationTimePassedThrough = source.LastStationTimePassedThrough,
        //        NextStationTimePassedThrough = source.NextStationTimePassedThrough,
        //        IsActive=source.IsActive,
        //        DriverID = source.DriverID
        //    };
        //}
        //public static BusLineDAO Clone(this BusLineDAO source)
        //{
        //    return new BusLineDAO
        //    {
        //        LineNumber = source.LineNumber,
        //        Area = source.Area,
        //        FirstStationNumber = source.FirstStationNumber,
        //        LastStationNumber = source.LastStationNumber,
        //        Deleted = source.Deleted
        //    };
        //}
        //public static StationLineDAO Clone(this StationLineDAO source)
        //{
        //    return new StationLineDAO
        //    {
        //        LineNumber = source.LineNumber,
        //        StationNumber = source.StationNumber,
        //        NumberStationInLine = source.NumberStationInLine,
        //        Deleted = source.Deleted
        //    };
        //}



    }

}
