using Nether.Ingest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Cosmos
{
    public class DefaultLeaderboardCosmosDBOutputManager : IOutputManager
    {
        private CsvMessageFormatter scoreSerializer;

        public DefaultLeaderboardCosmosDBOutputManager(CsvMessageFormatter scoreSerializer)
        {
            this.scoreSerializer = scoreSerializer;
        }

        public Task FlushAsync(string partitionId)
        {
            throw new NotImplementedException();
        }

        public Task OutputMessageAsync(string partitionId, string pipelineName, int index, Message msg)
        {
            throw new NotImplementedException();
        }
    }
}
