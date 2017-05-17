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

        public SqlDatabaseOutputManager(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;

            _context = new SqlMessageContext<T>(_connectionString, _tableName);
            _context.Database.EnsureCreated();
        }

        private void EnsureCreated()
        {                    
        }        

        public Task FlushAsync()
        {
            return Task.CompletedTask;
        }

        public async Task OutputMessageAsync(string pipelineName, int idx, Message msg)
        {
            T obj = CreateMessageObject(msg);
            await _context.Messages.AddAsync(obj);
            await _context.SaveChangesAsync();           
        }

        private T CreateMessageObject(Message msg)
        {
            T obj =  (T)Activator.CreateInstance(typeof(T));
            obj.SetProperties(msg.Properties);
            return obj;
        }
       
    }        
}

