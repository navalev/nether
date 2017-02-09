using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
