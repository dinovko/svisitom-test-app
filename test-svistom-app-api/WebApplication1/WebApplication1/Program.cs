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

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<Models.AppContext>();

                #region загрузка данных в память
                using (var context2 = new Models.AppContext(services.GetRequiredService<DbContextOptions<Models.AppContext>>()))
                {
                    // Look for any board games.
                    if (context.TableDatas.Any())
                    {
                        return;   // Data was already seeded
                    }
                    Random rndNum = new Random();
                    Random rndDate = new Random();
                    DateTime RandomDay()
                    {
                        DateTime start = new DateTime(1990, 1, 1);
                        int range = (DateTime.Today - start).Days;
                        return start.AddDays(rndDate.Next(range));
                    }
                    context2.TableDatas.AddRange(
                        new Models.TableData
                        {
                            Id = 1,
                            Number = rndNum.Next(99, 200),
                            Name = "Ivan",
                            Date = RandomDay(),
                            Status = true,
                        },
                        new Models.TableData
                        {
                            Id = 2,
                            Number = rndNum.Next(99, 200),
                            Name = "Vasya",
                            Date = RandomDay(),
                            Status = false,
                        },
                        new Models.TableData
                        {
                            Id = 3,
                            Number = rndNum.Next(99, 200),
                            Name = "Demon",
                            Date = RandomDay(),
                            Status = false,
                        },
                        new Models.TableData
                        {
                            Id = 4,
                            Number = rndNum.Next(99, 200),
                            Name = "Surgut",
                            Date = RandomDay(),
                            Status = true,
                        },
                        new Models.TableData
                        {
                            Id = 5,
                            Number = rndNum.Next(99, 200),
                            Name = "Volodya",
                            Date = RandomDay(),
                            Status = true,
                        },
                        new Models.TableData
                        {
                            Id = 6,
                            Number = rndNum.Next(99, 200),
                            Name = "Vladislava",
                            Date = RandomDay()
                        }
                        );

                    context2.SaveChanges();
                }
                #endregion

            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
