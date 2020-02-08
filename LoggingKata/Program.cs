using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath); //Calling all locations

            var parser = new TacoParser(); // Creating TacoParser object

            var locations = lines.Select(parser.Parse).ToArray(); // Creating array for both locations

            ITrackable location1 = locations[0]; // Instantiating variables for final answer 
            ITrackable location2 = locations[0];

            GeoCoordinate CorA = new GeoCoordinate(location1.Location.Latitude, location1.Location.Longitude); 
            GeoCoordinate CorB = new GeoCoordinate(location2.Location.Latitude, location2.Location.Longitude);

            double distance = CorA.GetDistanceTo(CorB);

            for (int i = 0; i < locations.Length; i++) // Looping through all locations, finding longest distance
            {
                for (int x = i + 1; x < locations.Length; x++)
                {
                    GeoCoordinate locA = new GeoCoordinate(locations[i].Location.Latitude, locations[i].Location.Longitude);
                    GeoCoordinate locB = new GeoCoordinate(locations[x].Location.Latitude, locations[x].Location.Longitude);

                    double locDistance = locA.GetDistanceTo(locB);

                    if (locDistance > distance)
                    {
                        distance = locDistance;
                        location1 = locations[i];
                        location2 = locations[x];
                    }
                }
            }
            Console.WriteLine($"Location 1: {location1.Name}"); // Printing answer
            Console.WriteLine($"Location 2: {location2.Name}");
            Console.WriteLine(distance);
        }
    }
}