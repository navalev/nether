using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Analytics.SqlDatabase
{
    public class SqlDatabaseOutputManager : IOutputManager
    {
        private SqlMessageContext _context;        

        // table structure:
        // ID  | MessageId  | Property | Value 
        public SqlDatabaseOutputManager(string connectionString, string tableName)
        {            
            _context = new SqlMessageContext(connectionString, tableName);
            _context.Database.EnsureCreated();
        }

        public Task FlushAsync()
        {
            return Task.CompletedTask;
        }

        public async Task OutputMessageAsync(string pipelineName, int idx, Message msg)
        {
            
            foreach (string key in msg.Properties.Keys)
            {
                var value = msg.Properties[key];

                await _context.Messages.AddAsync(new MessageValue { MessageId = msg.Id, Property = key, Value = value });
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
