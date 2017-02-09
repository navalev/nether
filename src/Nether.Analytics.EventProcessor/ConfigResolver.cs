using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Analytics.EventProcessor
{
    public static class ConfigResolver
    {
        public static string Resolve(string name)
        {
            var configVar = Environment.GetEnvironmentVariable(name);
            string configuration = configVar != null
                                    ? configVar
                                    : ConfigurationManager.AppSettings[name].ToString();
            return configuration;
        }
    }
}
