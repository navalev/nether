using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Analytics.SqlDatabase
{
    public class SqlDatabaseOutputManager<T> : IOutputManager where T : class, ISqlMessage
    {
        private static SqlMessageContext<T> _context;
        private string _connectionString;
        private string _tableName;     
        
        private SqlMessageContext<T> Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new SqlMessageContext<T>(_connectionString, _tableName);

                    //T obj = (T)Activator.CreateInstance(typeof(T));                    
                    //_context.Database.ExecuteSqlCommand(obj.GetCreateTableSql(_tableName));
                    _context.Database.EnsureCreated();
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
            //T obj = CreateMessageObject(msg);
            var obj = new SqlMessage();
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

