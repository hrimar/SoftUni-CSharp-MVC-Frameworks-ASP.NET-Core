using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyLibrary.Data;

namespace MyLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
       
            CreateWebHostBuilder(args).Build().Run();

            //// Var.1 - when we give ConnectionString in LibraryDbContext.cs:
            //Console.WriteLine("Initializing database...");

            //using (var context = new LibraryDbContext())
            //{
            //    context.Database.Migrate();// in EF the Migration has been automatic
            //}
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
