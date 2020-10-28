using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GestionVentas.Infraestructura.Extensions
{
    public static class EFConfiguration
    {
        private const string connectionString = "Server=127.0.0.1;Database=erp;Uid=root;Pwd=;";
        public static void AddCustomEFConfiguration(this IServiceCollection services) {

            services.AddDbContextPool<ApplicationContext>(x => x.UseMySQL(connectionString));
            services.AddTransient<IDbConnection>(x => new MySqlConnection(connectionString));
        }
    }
}
