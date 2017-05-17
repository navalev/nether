using System;
using System.Collections.Generic;

namespace Nether.Analytics.SqlDatabase
{
    public interface IMessage
    {
        Guid Id { get; set; }
        string MessageId { get; set; }

        void SetProperties(Dictionary<string, string> properties);
    }
}