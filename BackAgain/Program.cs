using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BackAgain.Service.Interface;
using BackAgain.Data;

namespace BackAgain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

           //Clear the Transaction Table before running the server
            using (var scope = webHost.Services.CreateScope())
            {
                var DBContext = (ProjContext)scope.ServiceProvider.GetRequiredService(typeof(ProjContext));
                DBContext._Transaction.RemoveRange(DBContext._Transaction.ToList());
                DBContext._TransactionAffiliate.RemoveRange(DBContext._TransactionAffiliate.ToList());

                DBContext.SaveChanges();
            }
        
            webHost.Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
