using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer
{
   public class AppConfiguration
    {
        public static string LocalGuideConnectionString { get; set; }
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            LocalGuideConnectionString = root.GetSection("ConnectionStrings").GetSection("LocalGuideDataBase").Value;
            var appSetting = root.GetSection("ApplicationSettings");
        }
        public  static string GetTokenInitializer()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var root = configurationBuilder.Build();
            string keyInitializer = root.GetSection("keys").GetSection("tokenKeyInitializer").Value;
            return keyInitializer;
        }
     }
       
}
