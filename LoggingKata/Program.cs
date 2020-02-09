using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Threading;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized.");
            Console.WriteLine("Running comparisons in 3...");
            Thread.Sleep(1000);
            Console.WriteLine("2...");
            Thread.Sleep(1000);
            Console.WriteLine("1...");
            Thread.Sleep(1000);

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
                    int counter = 0; // Counter to show comparisons. Not efficient but helps visualize what's happening for the sake of this exercise
                    counter += x;

                    GeoCoordinate locA = new GeoCoordinate(locations[i].Location.Latitude, locations[i].Location.Longitude);
                    GeoCoordinate locB = new GeoCoordinate(locations[x].Location.Latitude, locations[x].Location.Longitude);

                    double locDistance = locA.GetDistanceTo(locB);

                    Console.WriteLine($"Comparing location number {counter}...");

                    if (locDistance > distance) // Grabbing new location for answer if distance is greater than the previous comparison
                    {
                        distance = locDistance;
                        location1 = locations[i];
                        location2 = locations[x];
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Comparisons complete! Loading answer..."); // Printing answer
            Thread.Sleep(3000);

            Console.WriteLine("");

            Console.WriteLine("Answer:");
            Thread.Sleep(1000);
            Console.WriteLine($"Location 1: {location1.Name}");
            Thread.Sleep(500);
            Console.WriteLine($"Location 2: {location2.Name}");
            Thread.Sleep(500);
            Console.WriteLine($"Distance between coordinates: {distance}");

            Console.ReadLine();
        }
    }
}