using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            string[] cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogInfo("Something went wrong.");
                return null;
            }

            Point location = new Point();
            location.Latitude = Convert.ToDouble(cells[0]);
            location.Longitude = Convert.ToDouble(cells[1]);

            TacoBell tacoBell = new TacoBell();
            tacoBell.Name = cells[2];
            tacoBell.Location = location;

            return tacoBell;
        }
    }
}