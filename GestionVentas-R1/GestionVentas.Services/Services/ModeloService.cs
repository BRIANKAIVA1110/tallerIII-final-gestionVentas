using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ModeloService : IModeloService
    {
        private readonly IModeloRepository _modeloRepository;
        public ModeloService(IModeloRepository modeloRepository)
        {
            this._modeloRepository = modeloRepository;

        }

        public int AgregarModelo(ModeloDTO p_modeloDTO)
        {

            int result = this._modeloRepository.Add(new Modelo
            {
                Codigo = p_modeloDTO.Codigo,
                Descripcion = p_modeloDTO.Descripcion
            });

            return result;
        }

        public int ModificarModelo(ModeloDTO p_modeloDTO) {

            Modelo objEntity = this._modeloRepository.GetById(p_modeloDTO.Id);

            objEntity.Codigo = p_modeloDTO.Codigo;
            objEntity.Descripcion = p_modeloDTO.Descripcion;

            int result = this._modeloRepository.Update(objEntity);

            return result;
        }

        public int EliminarModelo(int p_id) {

            Modelo objEntity = this._modeloRepository.GetById(p_id);

            int result = this._modeloRepository.Delete(objEntity);

            return result;
        }

        public IEnumerable<ModeloDTO> getModelos()
        {
            var result = this._modeloRepository.Get()
                .Select(x => new ModeloDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });

            return result;
        }

        public ModeloDTO getModelo(int p_id)
        {
            Modelo objEntity = this._modeloRepository.GetById(p_id);
            ModeloDTO objResult = new ModeloDTO
            {
                Id = objEntity.Id,
                Codigo = objEntity.Codigo,
                Descripcion = objEntity.Descripcion
            };

            return objResult;
        }
    }
}
