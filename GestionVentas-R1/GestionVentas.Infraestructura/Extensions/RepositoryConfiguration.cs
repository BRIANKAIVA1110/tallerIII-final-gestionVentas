using GestionVentas.Infraestructura.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace GestionVentas.Infraestructura.Extensions
{
    public static class RepositoryConfiguration
    {
        public static void AddCustomRepositoryConfiguration(this IServiceCollection services) {

            services.AddTransient<IModeloRepository, ModeloRepository>();
        
        }
    }
}
