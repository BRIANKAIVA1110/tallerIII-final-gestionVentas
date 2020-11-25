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
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IArticuloRepository, ArticuloRepository>();
            services.AddTransient<IStockArticuloRepository, StockArticuloRepository>();
            services.AddTransient<IMarcaRepository, MarcaRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IVentaRepository, VentaRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IReporteRepository, ReporteRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IModuloRepository, ModuloRepository>();
        }
    }
}
