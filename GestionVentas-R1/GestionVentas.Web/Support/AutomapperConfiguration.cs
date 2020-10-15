using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Web.Models.ViewModels.Articulos;
using GestionVentas.Web.Models.ViewModels.Modelos;

namespace GestionVentas.Web.Support
{
    public class AutomapperConfiguration:Profile
    {
        public AutomapperConfiguration() {
            ModuloArticuloConfiguracion();
        }


        public void ModuloArticuloConfiguracion() {

            CreateMap<ArticuloViewModel, ArticuloDTO>().ReverseMap();
        }
    }
}
