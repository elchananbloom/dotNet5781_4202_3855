using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4202_3855
{
    //enum for the main menu.
    public enum Options { EXIT, ADD, DELETE, SEARCH, PRINT }
    //enum for the inner add options.
    public enum AddOptions { ADD_BUS = 1, ADD_STATION_TO_BUSLINE }
    //enum for the inner delete options.
    public enum DeletingOptions { DELETE_BUSLINE = 1, DELETE_STATION_FROM_BUSLINE }
    //enum for the inner searching options.
    public enum SearchingOptions { BUSSES_LINE = 1, OPTIONS_TRAVEL_BETWEEN_2_STATIONS }
    //enum for the inner printing options.
    public enum PrintingOptions { ALL_BUS_LINES = 1, STATIONS_LIST_AND_BUSLINES }
    //enum for the buslines area.
    public enum Areas { GENERAL = 1, NORTH, SOUTH, CENTER, JERUSALEM };
}
