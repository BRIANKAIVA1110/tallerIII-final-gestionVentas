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
        private readonly IModeloRepository _modeloRepository;
        private readonly IColorRepository _colorRepository;
        public ArticuloService(IArticuloRepository articuloRepository, IColorRepository colorRepository, IModeloRepository modeloRepository)
        {
            this._articuloRepository = articuloRepository;
            this._modeloRepository = modeloRepository;
            this._colorRepository= colorRepository;

        }

        public int AgregarArticulo(ArticuloDTO p_articuloDTO)
        {

            Articulo eArticulo = new Articulo();
            eArticulo.Color = this._colorRepository.GetById(p_articuloDTO.ColorId);
            eArticulo.Modelo = this._modeloRepository.GetById(p_articuloDTO.ModeloId);
            eArticulo.Codigo = p_articuloDTO.Codigo;
            eArticulo.Descripcion = p_articuloDTO.Descripcion;

            int result = this._articuloRepository.Add(eArticulo);
            return result;
        }

        public int ModificarArticulo(ArticuloDTO p_articuloDTO)
        {

            Articulo eArticulo = this._articuloRepository.GetById(p_articuloDTO.Id);

            if(eArticulo.Color.Id != p_articuloDTO.ColorId)
                eArticulo.Color = this._colorRepository.GetById(p_articuloDTO.ColorId);

            if (eArticulo.Modelo.Id != p_articuloDTO.ModeloId)
                eArticulo.Modelo = this._modeloRepository.GetById(p_articuloDTO.ModeloId);

            eArticulo.Codigo = p_articuloDTO.Codigo;
            eArticulo.Descripcion = p_articuloDTO.Descripcion;

            int result = this._articuloRepository.Update(eArticulo);

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
            var listArticuloDTO = this._articuloRepository.Get()
                .Select(x => new ArticuloDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    ModeloId = x.Modelo.Id,
                    ColorId = x.Color.Id,
                    ModeloDescripcion = $"{x.Modelo.Codigo} - {x.Modelo.Descripcion}",
                    ColorDescripcion = $"{x.Color.Codigo} - {x.Color.Descripcion}"
                });

            return listArticuloDTO;
        }


        public ArticuloDTO getArticulo(int p_id)
        {
            Articulo eArticulo = this._articuloRepository.GetById(p_id);
            ArticuloDTO articuloDto = new ArticuloDTO
            {
                Id = eArticulo.Id,
                Codigo = eArticulo.Codigo,
                Descripcion = eArticulo.Descripcion,
                ModeloId = eArticulo.Modelo.Id,
                ColorId  = eArticulo.Color.Id,
                ModeloDescripcion = $"{eArticulo.Modelo.Codigo} - {eArticulo.Modelo.Descripcion}",
                ColorDescripcion = $"{eArticulo.Color.Codigo} - {eArticulo.Color.Descripcion}"

            };

            return articuloDto;
        }
    }
}
