using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.DataAccess.Queries;
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
        private readonly IArticuloRepository _articuloRepository;
        public StockArticuloService(IStockArticuloRepository stockArticuloRepository, IArticuloRepository articuloRepository) {
            this._stockArticuloRepository = stockArticuloRepository;
            this._articuloRepository = articuloRepository;
        }

        public int AgregarStockArticulo(StockArticuloDTO p_stockArticuloDTO)
        {

            int result = this._stockArticuloRepository.Add(new StockArticulo
            {
                Articulo = this._articuloRepository.GetById(p_stockArticuloDTO.ArticuloId),
                Cantidad = p_stockArticuloDTO.Cantidad,
            });

            return result;
        }

        public int ModificarStockArticulo(StockArticuloDTO p_stockArticuloDTO)
        {

            StockArticulo objEntity = this._stockArticuloRepository.GetById(p_stockArticuloDTO.Id);

            objEntity.Articulo = this._articuloRepository.GetById(p_stockArticuloDTO.ArticuloId);
            objEntity.Cantidad= p_stockArticuloDTO.Cantidad;

            int result = this._stockArticuloRepository.Update(objEntity);

            return result;
        }

        public int EliminarStockArticulo(int p_id)
        {

            StockArticulo objEntity = this._stockArticuloRepository.GetById(p_id);

            int result = this._stockArticuloRepository.Delete(objEntity);

            return result;
        }
        public IEnumerable<StockArticuloDTO> getStockArticulos()
        {
            IEnumerable<StockArticuloDTO> listStockArticulo = this._stockArticuloRepository.Get()
                    .Select(x => new StockArticuloDTO
                    {
                        Id = x.Id,
                        ArticuloId = x.Articulo.Id,

                        ArticuloDescripcion = $"{x.Articulo.Descripcion} - " +
                        $"{x.Articulo.Modelo.Descripcion} - " +
                        $"{x.Articulo.Color.Descripcion}",

                        Cantidad = x.Cantidad
                    });

            return listStockArticulo;
        }

        public StockArticuloDTO getStockArticulo(int p_id)
        {
            StockArticulo objEntity = this._stockArticuloRepository.GetById(p_id);
            StockArticuloDTO objResult = new StockArticuloDTO
            {
                Id = objEntity.Id,
                ArticuloId = objEntity.Articulo.Id,

                ArticuloDescripcion = $"{objEntity.Articulo.CodigoBarras} - " +
                $"{objEntity.Articulo.Modelo.Descripcion} - " +
                $"{objEntity.Articulo.Color.Descripcion} - " +
                $"{objEntity.Articulo.Marca.Descripcion} - " +
                $"{objEntity.Articulo.Categoria.Descripcion}",

                Cantidad = objEntity.Cantidad
            };

            return objResult;
        }

        public virtual IEnumerable<ArticuloDTO> ObtenerArticulosSinAsignacionStock()
        {

            IEnumerable<ArticuloDTO> result = this._stockArticuloRepository.ExecuteQuery(new ObtenerArticulosSinAsignacionStock());

            return result;
        }
    }
}
