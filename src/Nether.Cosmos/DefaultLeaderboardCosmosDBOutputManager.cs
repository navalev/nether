// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Nether.Ingest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Cosmos
{
    public class DefaultLeaderboardCosmosDBOutputManager : IOutputManager
    {
        private CsvMessageFormatter _scoreSerializer;
        private string _databaseName;
        private DocumentClient _client;

        private const string allScoresCollectionName = "AllScoresCollection";

        public DefaultLeaderboardCosmosDBOutputManager(CsvMessageFormatter scoreSerializer, string cosmosDbUrl, string cosmosDbKey, string databaseName)
        {
            _scoreSerializer = scoreSerializer;
            _databaseName = databaseName;

            _client = new DocumentClient(new Uri(cosmosDbUrl), cosmosDbKey);
            init();
        }

        private void init()
        {
            // Create the database
            _client.CreateDatabaseIfNotExistsAsync(new Database() { Id = _databaseName }).GetAwaiter().GetResult();

            // Create the collections
            _client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(_databaseName),
                new DocumentCollection { Id = allScoresCollectionName }).
                GetAwaiter()
                .GetResult();
        }

        public Task FlushAsync(string partitionId)
        {
            return Task.CompletedTask;
        }

        public async Task OutputMessageAsync(string partitionId, string pipelineName, int index, Message msg)
        {
            ScoreDocument score = new ScoreDocument
            {
                GameId = msg.Properties["gameId"],
                UserId = msg.Properties["userId"],
                Score = Int32.Parse(msg.Properties["score"])
            };

            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, allScoresCollectionName), score);
        }
    }

    internal class ScoreDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string GameId { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
    }
}
