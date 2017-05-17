using System;

namespace Nether.Analytics.SqlDatabase
{
    public class MessageValue
    {
        public Guid Id { get; set; }
        public string MessageId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; } 
    }
}