using System;
using System.Collections.Generic;
using System.Text;

namespace Nether.Analytics.SqlDatabase
{   
    /// <summary>
    /// This is a sample ISqlMessage implemetation for geohash message type.    
    /// </summary>
    public class SqlMessage : ISqlMessage
    {            
        public Guid Id { get; set; }
        public string MessageId { get; set; }
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

        public string GetCreateTableSql(string tableName)
        {

            string createSql = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='" + tableName + "') " +
                        "CREATE TABLE " + tableName + " ( " +
                        "\"Id\" uniqueidentifier not null," +
                        "\"MessageId\" varchar(50)," +
                        "\"Type\" varchar(50), " +
                        "\"Version\" varchar(50), " +
                        "\"EnqueueTimeUtc\" varchar(50)," +
                        "\"GamesessionId\" varchar(50)," +
                        "\"Lat\" varchar(50), " +
                        "\"Lon\" varchar(50)," +
                        "\"GeoHash\" varchar(50)," +
                        "\"GeoHashPrecision\" varchar(50)," +
                        "\"GeoHashCenterLat\" varchar(50)," +
                        "\"GeoHashCenterLon\" varchar(50)," +
                        "\"Rnd\" varchar(50))";

            return createSql;
        }

        public void SetProperties(Dictionary<string, string> properties)
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
