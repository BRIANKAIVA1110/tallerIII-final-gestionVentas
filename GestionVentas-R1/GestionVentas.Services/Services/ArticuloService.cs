using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ArticuloService: IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;
        public ArticuloService(IArticuloRepository articuloRepository)
        {
            this._articuloRepository = articuloRepository;

        }

        public int AgregarArticulo(ArticuloDTO p_articuloDTO)
        {

            int result = this._articuloRepository.Add(new Articulo
            {
                Codigo = p_articuloDTO.Codigo,
                Descripcion = p_articuloDTO.Descripcion
            });

            return result;
        }

        public int ModificarArticulo(ArticuloDTO p_articuloDTO)
        {

            Articulo objEntity = this._articuloRepository.GetById(p_articuloDTO.Id);

            objEntity.Codigo = p_articuloDTO.Codigo;
            objEntity.Descripcion = p_articuloDTO.Descripcion;

            int result = this._articuloRepository.Update(objEntity);

            return result;
        }

        public int EliminarArticulo(int p_id)
        {

            Articulo objEntity = this._articuloRepository.GetById(p_id);

            int result = this._articuloRepository.Delete(objEntity);

            return result;
        }

        public IEnumerable<ArticuloDTO> getArticulos()
        {
            var result = this._articuloRepository.Get()
                .Select(x => new ArticuloDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });

            return result;
        }


        public ArticuloDTO getArticulo(int p_id)
        {
            Articulo objEntity = this._articuloRepository.GetById(p_id);
            ArticuloDTO objResult = new ArticuloDTO
            {
                Id = objEntity.Id,
                Codigo = objEntity.Codigo,
                Descripcion = objEntity.Descripcion
            };

            return objResult;
        }
    }
}
