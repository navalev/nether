using System;
using System.Collections.Generic;
using System.Text;

namespace Nether.Analytics.SqlDatabase
{   
    /// <summary>
    /// This is a sample implemetation for Geo hash message type, for the SqlMessageBase abstract class
    /// </summary>
    public class SqlMessage : SqlMessageBase
    {                    
        public string Type { get; set; }
        public string Version { get; set; }
        public string EnqueueTimeUtc { get; set; }
        public string GamesessionId { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string GeoHash { get; set; }
        public string GeoHashPrecision { get; set; }
        public string GeoHashCenterLat { get; set; }
        public string GeoHashCenterLon { get; set; }
        public string Rnd { get; set; }           

        public override void SetProperties(Dictionary<string, string> properties)
        {
            MessageId = properties["id"];
            Type = properties["type"];
            Version = properties["version"];
            EnqueueTimeUtc = properties["enqueueTimeUtc"];
            GamesessionId = properties["gameSession"];
            Lat = properties["lat"];
            Lon = properties["lon"];
            GeoHash = properties["geoHash"];
            GeoHashPrecision = properties["geoHashPrecision"];
            GeoHashCenterLat = properties["geoHashCenterLat"];
            GeoHashCenterLon = properties["geoHashCenterLon"];
            Rnd = properties["rnd"];
        }
    }
}
