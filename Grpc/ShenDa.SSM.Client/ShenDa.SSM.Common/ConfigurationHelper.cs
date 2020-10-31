using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ShenDa.SSM.Common
{
    public class ConfigurationHelper
    {

        public static T Get<T>(string sectionName, string fileName = "appsettings.json")
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile(fileName)
                       .Build();

            return configuration.GetSection(sectionName).Get<T>();
        }


        public static T GetByKey<T>(string key, string fileName = "appsettings.json")
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile(fileName)
                       .Build();
            return configuration.GetValue<T>(key);
        }
    }
}
