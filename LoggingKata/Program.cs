using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Collections.Generic;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            ITrackable tempA;
            ITrackable tempB;
            ITrackable locA = null;
            ITrackable locB = null;

            //ITrackable tempC;
            //ITrackable tempD;
            //ITrackable locC = null;
            //ITrackable locD = null;

            // Create a `double` variable to store the distance

            double tempDistance;
            double distance;
            double greatestDistance = 0;
            //double smallestDistance = 500;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length; i++)
            {
                // Create a new corA Coordinate with your locA's lat and long
                tempA = locations[i];

                double latitudeA = tempA.Location.Latitude;
                double longitudeA = tempA.Location.Longitude;

                GeoCoordinate coordinateA = new GeoCoordinate(latitudeA, longitudeA);

                //tempC = locations[i];
                //double latitudeC = tempC.Location.Latitude;
                //double longitudeC = tempC.Location.Longitude;
                //GeoCoordinate coordinateC = new GeoCoordinate(latitudeC, longitudeC);


                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int j = i; j < locations.Length; j++)
                {
                    // Create a new Coordinate with your locB's lat and long
                    tempB = locations[j];

                    double latitudeB = tempB.Location.Latitude;
                    double longitudeB = tempB.Location.Longitude;

                    GeoCoordinate coordinateB = new GeoCoordinate(latitudeB, longitudeB);

                    //tempD = locations[j];
                    //double latitudeD = tempD.Location.Latitude;
                    //double longitudeD = tempD.Location.Longitude;
                    //GeoCoordinate coordinateD = new GeoCoordinate(latitudeD, longitudeD);


                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    tempDistance = coordinateA.GetDistanceTo(coordinateB);

                    //if (i != j)
                    //{
                    //    distance = coordinateC.GetDistanceTo(coordinateD);
                    //}
                    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                    if (tempDistance > greatestDistance)
                    {
                        locA = tempA;
                        locB = tempB;
                        greatestDistance = tempDistance;
                    }

                    //if (distance < smallestDistance)
                    //{
                    //    locC = tempC;
                    //    locD = tempD;
                    //    smallestDistance = tempDistance;
                    //}
                }
            }
            Console.WriteLine();
            Console.WriteLine("############################################");
            Console.WriteLine();

            logger.LogInfo($"The two Taco Bells that are the furhest apart are {locA.Name} and \n {locB.Name} " +
                           $"at approximatly {Math.Round(greatestDistance / 1609)} miles or {Math.Round(greatestDistance / 1000)} kilometers.");
            Console.WriteLine();
            //logger.LogInfo($"The two Taco Bells that are the closest together are {locC.Name} and \n {locD.Name} " +
            //               $"at approximatly {smallestDistance} meters.");

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
        }
    }
}
