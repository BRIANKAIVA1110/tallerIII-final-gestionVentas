using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Extensions
{
    public static class EFConfiguration
    {
        private const string connectionString = "Server=127.0.0.1;Database=erp;Uid=root;Pwd=;";
        public static void AddCustomEFConfiguration(this IServiceCollection services) {

            services.AddDbContextPool<ApplicationContext>(x => x.UseMySQL(connectionString));
        }
    }
}
