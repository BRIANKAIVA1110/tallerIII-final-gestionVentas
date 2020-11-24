using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Web.Models.ViewModels.Articulos;
using GestionVentas.Web.Models.ViewModels.Autenticacion;
using GestionVentas.Web.Models.ViewModels.Stock;
using GestionVentas.Web.Models.ViewModels.Ventas;

namespace GestionVentas.Web.Support
{
    public class AutomapperConfiguration:Profile
    {
        public AutomapperConfiguration() {
            ModuloArticuloConfiguracion();
            ModuloStockConfiguration();
            ModuloSeguridadConfiguration();
            ModuloVentasConfiguracion();
        }

        public void ModuloArticuloConfiguracion() {

            CreateMap<ArticuloViewModel, ArticuloDTO>().ReverseMap();
            CreateMap<ModeloViewModel, ModeloDTO>().ReverseMap();
            CreateMap<ColorViewModel, ColorDTO>().ReverseMap();
            CreateMap<MarcaViewModel, MarcaDTO>().ReverseMap();
            CreateMap<CategoriaViewModel, CategoriaDTO>().ReverseMap();
        }
        public void ModuloStockConfiguration() {
            CreateMap<StockViewModel, StockArticuloDTO>().ReverseMap();
        }
        public void ModuloSeguridadConfiguration()
        {
            CreateMap<AutenticacionViewModel, UsuarioDTO>().ReverseMap();
        }
        public void ModuloVentasConfiguracion() {
            CreateMap<VentaViewModel, VentaDTO>().ReverseMap();
        }
    }
}
