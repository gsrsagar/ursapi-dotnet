
using LocalGuideAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using LocalGuideAPI.Models.Implementaions;
using Microsoft.Extensions.Configuration;
namespace urs_api.DbContexts
{
    public  class CustomDbContext
    {
        static CustomDbContext()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:URSDataBase"];

            DbContextOptions = new DbContextOptionsBuilder<URSDbContext>().UseSqlServer(connectionString)
                .Options;
        }

        public static DbContextOptions<URSDbContext> DbContextOptions { get; }

    }
}
