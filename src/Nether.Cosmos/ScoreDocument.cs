using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nether.Cosmos
{
    public class ScoreDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Country { get; set; }

        public int Score { get; set; }
    }
}
