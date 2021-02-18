namespace DAL
{
    public static class Configuration
    {
        private static int serialBusInTravel = 0;
        private static int serialBusLine = 0;
        private static int serialLineInService = 0;

        public static int SerialBusInTravel => ++serialBusInTravel;
        public static int SerialBusLine => ++serialBusLine;
        public static int SerialLineInService => ++serialLineInService;
    }
}