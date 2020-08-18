using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ModeloService
    {
        private readonly IModeloRepository _modeloRepository;
        public ModeloService(IModeloRepository modeloRepository) {
            this._modeloRepository = modeloRepository;

        }


        public int AgregarModelo(ModeloDTO p_modeloDTO) {

            int result = this._modeloRepository.Add(new Modelo
                        {
                            Codigo = p_modeloDTO.Codigo,
                            Descripcion = p_modeloDTO.Descripcion
                        });

            return result;
        }


        public IEnumerable<ModeloDTO> getModelos() {
            var result = this._modeloRepository.Get()
                .Select( x => new ModeloDTO { 
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });

            return result;
        }

        public ModeloDTO getModelo(int p_id) {
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
