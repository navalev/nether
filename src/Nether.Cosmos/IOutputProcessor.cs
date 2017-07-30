﻿using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Nether.Ingest;

namespace Nether.Cosmos
{
    /// <summary>
    /// Process messages before wiritng to cosmos db
    /// </summary>
    public interface IOutputProcessor
    {
        void Set(string databaseName, string collectionName, DocumentClient client);
        Task ProcessMessage(Message msg);
    }
}