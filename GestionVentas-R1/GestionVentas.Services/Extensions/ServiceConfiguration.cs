using GestionVentas.Infraestructura.Repositories;
using GestionVentas.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Extensions
{
    public static class ServiceConfiguration
    {

        public static void AddCustomServiceConfiguration(this IServiceCollection services) {
            services.AddTransient<IModeloService, ModeloService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IArticuloService, ArticuloService>();
            services.AddTransient<IStockArticuloService, StockArticuloService>();
            services.AddTransient<IMarcaService, MarcaService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
            services.AddTransient<IVentaService, VentaService>();
            services.AddTransient<ISeguridadService, SeguridadService>();
            services.AddTransient<IReporteRepository, ReporteRepository>();
        }
    }
}
