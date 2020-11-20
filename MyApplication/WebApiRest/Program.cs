using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebApiRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var hostsever = CreateHostBuilder(args).Build();
            using (var ambiente = hostsever.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                try
                {
                    var user = services.GetRequiredService<UserManager<Usuario>>();
                    var Context = services.GetRequiredService<ContextoCurso>();
                    Context.Database.Migrate();
                    DataPrueba.Insertar(Context, user).Wait();
                }
                catch (Exception ex)
                {
                    var log = services.GetRequiredService<ILogger>();
                    log.LogError(ex, "ha ocurrido un error en la migracion");
                    throw;
                }
            }
            hostsever.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
