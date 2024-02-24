using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Models
{
    public static class ConfigurationHelper
    {

        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}