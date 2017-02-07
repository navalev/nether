using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Configuration;

namespace Nether.Analytics.EventProcessor
{
    class NameResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
