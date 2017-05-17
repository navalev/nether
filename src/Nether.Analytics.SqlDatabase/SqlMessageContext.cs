using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nether.Analytics.SqlDatabase
{
    class SqlMessageContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public DbSet<MessageValue> Messages { get; set; }

        public SqlMessageContext(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MessageValue>()
                .HasKey(c => c.Id);

            builder.Entity<MessageValue>().Property(m => m.Id).HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.GuidValueGenerator>();

            builder.Entity<MessageValue>()
                .ForSqlServerToTable(_tableName);            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlServer(_connectionString);
        }

    }
}
