using AutoMapper;
using GestionVentas.Domain.Entities;
using GestionVentas.Web.Models.ViewModels.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Support.TypeConverters
{
    public class ModeloViewModelTypeConverter : ITypeConverter<Modelo, ModeloViewModel>
    {
        public ModeloViewModel Convert(Modelo source, ModeloViewModel destination, ResolutionContext context)
        {
            destination = new ModeloViewModel();

            if (source!=null) {
                destination.Id = source.Id;
                destination.Codigo = source.Codigo;
                destination.Descripcion = source.Descripcion;
            }

            return destination;
        }
    }
}
