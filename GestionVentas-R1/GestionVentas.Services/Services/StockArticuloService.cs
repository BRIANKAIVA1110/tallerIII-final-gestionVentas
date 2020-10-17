using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class StockArticuloService : IStockArticuloService
    {
        private readonly IStockArticuloRepository _stockArticuloRepository;
        public StockArticuloService(IStockArticuloRepository stockArticuloRepository) {
            this._stockArticuloRepository = stockArticuloRepository;
        }


        public IEnumerable<StockArticuloDTO> obtenerStockArticulos()
        {
            IEnumerable<StockArticuloDTO> listStockArticulo = this._stockArticuloRepository.Get()
                    .Select(x => new StockArticuloDTO
                    {
                        Id = x.Id,
                        ArticuloId = x.Articulo.Id,
                        ArticuloDescripcion = $"{x.Articulo.Descripcion} - {x.Articulo.Modelo.Descripcion} - {x.Articulo.Color.Descripcion}",
                        Cantidad = x.Cantidad
                    });

            return listStockArticulo;
        }
    }
}
