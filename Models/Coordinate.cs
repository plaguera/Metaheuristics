namespace Metaheuristics.Models
{
    public class Coordinate
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", Latitude, Longitude);
        }
    }
}
