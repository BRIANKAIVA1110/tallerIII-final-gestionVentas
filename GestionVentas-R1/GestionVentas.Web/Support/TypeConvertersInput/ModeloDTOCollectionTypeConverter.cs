using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Web.Models.ViewModels.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Support.TypeConvertersInput
{
    public class ModeloDTOCollectionTypeConverter : ITypeConverter<List<ModeloViewModel>, List<ModeloDTO>>
    {
        public List<ModeloDTO> Convert(List<ModeloViewModel> source, List<ModeloDTO> destination, ResolutionContext context)
        {
            destination = new List<ModeloDTO>();

            if (source.Any()) {

                foreach (var item in source)
                {
                    destination.Add(new ModeloDTO
                    {
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
