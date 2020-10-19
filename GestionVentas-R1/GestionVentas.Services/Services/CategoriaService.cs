using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            this._categoriaRepository = categoriaRepository;
        }

        public int AgregarCategoria(CategoriaDTO p_CategoriaDTO)
        {

            int result = this._categoriaRepository.Add(new Categoria
            {
                Codigo = p_CategoriaDTO.Codigo,
                Descripcion = p_CategoriaDTO.Descripcion
            });

            return result;
        }

        public int ModificarCategoria(CategoriaDTO p_CategoriaDTO)
        {

            Categoria objEntity = this._categoriaRepository.GetById(p_CategoriaDTO.Id);

            objEntity.Codigo = p_CategoriaDTO.Codigo;
            objEntity.Descripcion = p_CategoriaDTO.Descripcion;

            int result = this._categoriaRepository.Update(objEntity);

            return result;
        }

        public int EliminarCategoria(int p_id)
        {

            Categoria objEntity = this._categoriaRepository.GetById(p_id);

            int result = this._categoriaRepository.Delete(objEntity);

            return result;
        }

        public IEnumerable<CategoriaDTO> getCategorias()
        {
            var result = this._categoriaRepository.Get()
                .Select(x => new CategoriaDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });

            return result;
        }

        public CategoriaDTO getCategoria(int p_id)
        {
            Categoria objEntity = this._categoriaRepository.GetById(p_id);
            CategoriaDTO objResult = new CategoriaDTO
            {
                Id = objEntity.Id,
                Codigo = objEntity.Codigo,
                Descripcion = objEntity.Descripcion
            };

            return objResult;
        }
    }
}
