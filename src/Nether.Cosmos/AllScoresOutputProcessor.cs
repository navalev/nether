using Microsoft.Azure.Documents.Client;
using Nether.Ingest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Cosmos
{
    public class AllScoresOutputProcessor : IOutputProcessor
    {
        private string _collectionName;
        private string _databaseName;
        private DocumentClient _client;

        public async Task ProcessMessage(Message msg)
        {
            ScoreDocument score = new ScoreDocument { UserId = msg.Properties["userId"], Score = Convert.ToInt32(msg.Properties["score"])};
            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), score);
        }

        public void Set(string databaseName, string collectionName, DocumentClient client)
        {
            _databaseName = databaseName;
            _collectionName = collectionName;
            _client = client;
        }
    }
}
