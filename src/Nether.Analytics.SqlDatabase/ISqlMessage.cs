using System;
using System.Collections.Generic;

namespace Nether.Analytics.SqlDatabase
{
    /// <summary>
    /// This is an interface for SQL messages.
    /// Each message will have at least an Id and message ID. implemetion classes will add additional properties
    /// </summary>s   
    public interface ISqlMessage
    {
        Guid Id { get; set; }
        string MessageId { get; set; }

        // set the object properties values from the message properties
        void SetProperties(Dictionary<string, string> properties);

        // return a sql to create the table to hold message content
        string GetCreateTableSql(string tableName);
    }
}