using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly IModeloRepository _modeloRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public ArticuloService(IArticuloRepository articuloRepository, IColorRepository colorRepository, IModeloRepository modeloRepository, ICategoriaRepository categoriaRepository, IMarcaRepository marcaRepository)
        {
            this._articuloRepository = articuloRepository;
            this._modeloRepository = modeloRepository;
            this._colorRepository = colorRepository;
            this._marcaRepository = marcaRepository;
            this._categoriaRepository = categoriaRepository;
        }

        public int AgregarArticulo(ArticuloDTO p_articuloDTO)
        {

            Articulo eArticulo = new Articulo();
            eArticulo.Color = this._colorRepository.GetById(p_articuloDTO.ColorId);
            eArticulo.Modelo = this._modeloRepository.GetById(p_articuloDTO.ModeloId);
            eArticulo.Marca = this._marcaRepository.GetById(p_articuloDTO.MarcaId);
            eArticulo.Categoria = this._categoriaRepository.GetById(p_articuloDTO.CategoriaId);
            eArticulo.CodigoBarras = GenerarCodigoBarras(eArticulo.Modelo.Codigo, eArticulo.Color.Codigo, eArticulo.Marca.Codigo, eArticulo.Categoria.Codigo);
            eArticulo.Descripcion = p_articuloDTO.Descripcion;
            eArticulo.Precio = p_articuloDTO.Precio;
            int result = this._articuloRepository.Add(eArticulo);
            return result;
        }

        private string GenerarCodigoBarras(string codModelo, string codColor, string codMarca, string codCategoria) {
            if (codModelo != null && codColor != null && codMarca != null && codCategoria != null)
                return $"{codModelo}{codColor}{codMarca}{codCategoria}";
            else
                throw new Exception("ocurrio un error al generar el codigo de barras para el articulo");
        }


        public ArticuloDTO ObtenerArticuloPorCodigoBarras(string p_codigoBarras) {

            Articulo eArticulo = this._articuloRepository.Get().Where(x => x.CodigoBarras == p_codigoBarras).FirstOrDefault();

            ArticuloDTO articuloDto = new ArticuloDTO
            {
                Id = eArticulo.Id,
                CodigoBarras = eArticulo.CodigoBarras,
                Descripcion = eArticulo.Descripcion,
                ModeloId = eArticulo.Modelo.Id,
                ColorId = eArticulo.Color.Id,
                MarcaId = eArticulo.Marca.Id,
                CategoriaId = eArticulo.Categoria.Id,
                ModeloDescripcion = $"{eArticulo.Modelo.Codigo} - {eArticulo.Modelo.Descripcion}",
                ColorDescripcion = $"{eArticulo.Color.Codigo} - {eArticulo.Color.Descripcion}",
                MarcaDescripcion = $"{eArticulo.Marca.Codigo} - {eArticulo.Marca.Descripcion}",
                CategoriaDescripcion = $"{eArticulo.Categoria.Codigo} - {eArticulo.Categoria.Descripcion}",
                Precio = eArticulo.Precio,

            };

            return articuloDto;

        }
        public int ModificarArticulo(ArticuloDTO p_articuloDTO)
        {

            Articulo eArticulo = this._articuloRepository.GetById(p_articuloDTO.Id);

            if(eArticulo.Color.Id != p_articuloDTO.ColorId)
                eArticulo.Color = this._colorRepository.GetById(p_articuloDTO.ColorId);

            if (eArticulo.Modelo.Id != p_articuloDTO.ModeloId)
                eArticulo.Modelo = this._modeloRepository.GetById(p_articuloDTO.ModeloId);

            if (eArticulo.Marca.Id != p_articuloDTO.MarcaId)
                eArticulo.Marca = this._marcaRepository.GetById(p_articuloDTO.MarcaId);

            if (eArticulo.Categoria.Id != p_articuloDTO.CategoriaId)
                eArticulo.Categoria = this._categoriaRepository.GetById(p_articuloDTO.CategoriaId);

            eArticulo.CodigoBarras = GenerarCodigoBarras(eArticulo.Modelo.Codigo, eArticulo.Color.Codigo, eArticulo.Marca.Codigo, eArticulo.Categoria.Codigo);
            eArticulo.Descripcion = p_articuloDTO.Descripcion;
            eArticulo.Precio = p_articuloDTO.Precio;

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
                    CodigoBarras = x.CodigoBarras,
                    Descripcion = x.Descripcion,
                    ModeloId = x.Modelo.Id,
                    ColorId = x.Color.Id,
                    ModeloDescripcion = x.Modelo!=null? $"{x.Modelo.Codigo} - {x.Modelo.Descripcion}" : "",
                    ColorDescripcion = x.Color!=null? $"{x.Color.Codigo} - {x.Color.Descripcion}" : "",
                    MarcaDescripcion = x.Marca!=null? $"{x.Marca.Codigo} - {x.Marca.Descripcion}": "",
                    CategoriaDescripcion = x.Categoria!=null? $"{x.Categoria.Codigo} - {x.Categoria.Descripcion}": "",
                    Precio = x.Precio,
                });

            return listArticuloDTO;
        }


        public ArticuloDTO getArticulo(int p_id)
        {
            Articulo eArticulo = this._articuloRepository.GetById(p_id);
            ArticuloDTO articuloDto = new ArticuloDTO
            {
                Id = eArticulo.Id,
                CodigoBarras = eArticulo.CodigoBarras,
                Descripcion = eArticulo.Descripcion,
                ModeloId = eArticulo.Modelo.Id,
                ColorId  = eArticulo.Color.Id,
                MarcaId = eArticulo.Marca.Id,
                CategoriaId = eArticulo.Categoria.Id,
                ModeloDescripcion = $"{eArticulo.Modelo.Codigo} - {eArticulo.Modelo.Descripcion}",
                ColorDescripcion = $"{eArticulo.Color.Codigo} - {eArticulo.Color.Descripcion}",
                MarcaDescripcion = $"{eArticulo.Marca.Codigo} - {eArticulo.Marca.Descripcion}",
                CategoriaDescripcion = $"{eArticulo.Categoria.Codigo} - {eArticulo.Categoria.Descripcion}",
                Precio = eArticulo.Precio,

            };

            return articuloDto;
        }
    }
}
