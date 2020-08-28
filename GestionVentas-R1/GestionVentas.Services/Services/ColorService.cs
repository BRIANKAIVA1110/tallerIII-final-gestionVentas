using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ColorService:IColorService
    {
        private readonly IColorRepository _colorRepository;
        public ColorService(IColorRepository colorRepository) {
            this._colorRepository = colorRepository;
        }

        public int AgregarColor(ColorDTO p_colorDTO)
        {

            int result = this._colorRepository.Add(new Color
            {
                Codigo = p_colorDTO.Codigo,
                Descripcion = p_colorDTO.Descripcion
            });

            return result;
        }

        public int ModificarColor(ColorDTO p_colorDTO)
        {

            Color objEntity = this._colorRepository.GetById(p_colorDTO.Id);

            objEntity.Codigo = p_colorDTO.Codigo;
            objEntity.Descripcion = p_colorDTO.Descripcion;

            int result = this._colorRepository.Update(objEntity);

            return result;
        }

        public int EliminarColor(int p_id)
        {

            Color objEntity = this._colorRepository.GetById(p_id);

            int result = this._colorRepository.Delete(objEntity);

            return result;
        }

        public IEnumerable<ColorDTO> getColores()
        {
            var result = this._colorRepository.Get()
                .Select(x => new ColorDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                });

            return result;
        }

        public ColorDTO getColor(int p_id)
        {
            Color objEntity = this._colorRepository.GetById(p_id);
            ColorDTO objResult = new ColorDTO
            {
                Id = objEntity.Id,
                Codigo = objEntity.Codigo,
                Descripcion = objEntity.Descripcion
            };

            return objResult;
        }
    }
}
