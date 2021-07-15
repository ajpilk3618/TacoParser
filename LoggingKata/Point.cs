namespace LoggingKata
{
    public struct Point
    {
        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}