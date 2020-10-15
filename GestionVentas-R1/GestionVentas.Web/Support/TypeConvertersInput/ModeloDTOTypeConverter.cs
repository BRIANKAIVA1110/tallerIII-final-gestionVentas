using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Web.Models.ViewModels.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Support.TypeConvertersInput
{
    public class ModeloDTOTypeConverter : ITypeConverter<ModeloViewModel, ModeloDTO>
    {
        public ModeloDTO Convert(ModeloViewModel source, ModeloDTO destination, ResolutionContext context)
        {
            destination = new ModeloDTO();

            if (source != null) {
                destination.Id = source.Id;
                destination.Codigo = source.Codigo;
                destination.Descripcion = source.Descripcion;
            }

            return destination;
        }
    }
}
