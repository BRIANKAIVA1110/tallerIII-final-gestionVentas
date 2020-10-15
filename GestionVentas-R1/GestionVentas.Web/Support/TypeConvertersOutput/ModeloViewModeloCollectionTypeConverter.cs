using AutoMapper;
using GestionVentas.Domain.Entities;
using GestionVentas.Web.Models.ViewModels.Modelos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Support.TypeConverters
{
    public class ModeloViewModeloCollectionTypeConverter : ITypeConverter<List<Modelo>, List<ModeloViewModel>>
    {
        public List<ModeloViewModel> Convert(List<Modelo> source, List<ModeloViewModel> destination, ResolutionContext context)
        {
            destination = new List<ModeloViewModel>();

            if (source.Any()) {

                foreach (var item in source)
                {
                    destination.Add(new ModeloViewModel { 
                        Id = item.Id,
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion
                    });
                }
            }

            return destination;
        }
    }
}
