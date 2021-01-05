using A35Mge.Database;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
                var context = scope.ServiceProvider.GetRequiredService<A35MgeDbContext>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                try
                {
                    var Init = new Init(context, mapper);
                    //await Init.ExportData();
                    var shouldSeed = !await context.Database.CanConnectAsync();
                    logger.LogInformation("Migrating Database");
                    await context.Database.MigrateAsync();

                    if (shouldSeed)
                    {
                        logger.LogInformation($"{DateTime.Now} 初始化数据库初始化数据");
                        await Init.Seeds();
                    }
                    //这下面可以造一些初始数据
                    //        var students = new Student[]
                    //{
                    //    new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01")},
                    //    new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01")},
                    //    new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01")},
                    //    new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01")},
                    //    new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01")},
                    //    new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01")},
                    //    new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2018-09-01")},
                    //    new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01")}
                    //};

                    //        context.Students.AddRange(students);
                    //        context.SaveChanges();
                }
                catch (Exception err)
                {
                    logger.LogInformation(err, "Migration Failed");
                }
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
