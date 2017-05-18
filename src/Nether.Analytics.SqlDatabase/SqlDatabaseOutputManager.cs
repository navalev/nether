using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Analytics.SqlDatabase
{
    public class SqlDatabaseOutputManager<T> : IOutputManager where T : class, IMessage
    {
        private SqlMessageContext<T> _context;
        private string _connectionString;
        private string _tableName;     
        
        private SqlMessageContext<T> Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new SqlMessageContext<T>(_connectionString, _tableName);
                    _context.Database.Migrate();
                }
                return _context;
            }
        }
        
        public SqlDatabaseOutputManager(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;            
        }
      
        public Task FlushAsync()
        {
            return Task.CompletedTask;
        }

        public async Task OutputMessageAsync(string pipelineName, int idx, Message msg)
        {            
            T obj = CreateMessageObject(msg);
            await Context.Messages.AddAsync(obj);
            await Context.SaveChangesAsync();           
        }

        private T CreateMessageObject(Message msg)
        {
            T obj =  (T)Activator.CreateInstance(typeof(T));
            obj.SetProperties(msg.Properties);
            return obj;
        }
       
    }        
}

