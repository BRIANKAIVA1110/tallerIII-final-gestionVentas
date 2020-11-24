using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class MarcaService: IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(IMarcaRepository marcaRepository) {
            this._marcaRepository = marcaRepository;
        }

        public int AgregarMarca(MarcaDTO p_marcaDTO)
        {

            int result = this._marcaRepository.Add(new Marca
            {
                Codigo = p_marcaDTO.Codigo,
                Descripcion = p_marcaDTO.Descripcion
            });

            return result;
        }

        public int ModificarMarca(MarcaDTO p_marcaDTO)
        {

            Marca objEntity = this._marcaRepository.GetById(p_marcaDTO.Id);

            objEntity.Codigo = p_marcaDTO.Codigo;
            objEntity.Descripcion = p_marcaDTO.Descripcion;

            int result = this._marcaRepository.Update(objEntity);

            return result;
        }

        public int EliminarMarca(int p_id)
        {

            Marca objEntity = this._marcaRepository.GetById(p_id);

            int result = this._marcaRepository.Delete(objEntity);

            return result;
        }

        public IEnumerable<MarcaDTO> getMarcas()
        {
            var result = this._marcaRepository.Get()
                .Select(x => new MarcaDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });

            return result;
        }

        public MarcaDTO getMarca(int p_id)
        {
            Marca objEntity = this._marcaRepository.GetById(p_id);
            MarcaDTO objResult = new MarcaDTO
            {
                Id = objEntity.Id,
                Codigo = objEntity.Codigo,
                Descripcion = objEntity.Descripcion
            };

            return objResult;
        }

        public byte[] GenerarExportacionRegistros()
        {
            var result = this._marcaRepository.Get()
                .Select(x => new ColorDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });
            if (result.Any())
            {
                StringBuilder sb = new StringBuilder();
                string separador = ";";
                foreach (var item in result)
                {
                    sb.AppendLine($"{item.Codigo}{separador}{item.Descripcion}");
                }
                byte[] byteFile = Encoding.UTF8.GetBytes(sb.ToString());

                return byteFile;
            }

            return new byte[1];
        }

    }
}
