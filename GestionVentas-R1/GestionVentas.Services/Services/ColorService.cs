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
            try
            {
                int result = this._colorRepository.Add(new Color
                {
                    Codigo = p_colorDTO.Codigo,
                    Descripcion = p_colorDTO.Descripcion
                });

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public int ModificarColor(ColorDTO p_colorDTO)
        {
            try
            {
                Color objEntity = this._colorRepository.GetById(p_colorDTO.Id);

                objEntity.Codigo = p_colorDTO.Codigo;
                objEntity.Descripcion = p_colorDTO.Descripcion;

                int result = this._colorRepository.Update(objEntity);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public int EliminarColor(int p_id)
        {
            try
            {
                Color objEntity = this._colorRepository.GetById(p_id);

                int result = this._colorRepository.Delete(objEntity);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public IEnumerable<ColorDTO> getColores()
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public ColorDTO getColor(int p_id)
        {
            try
            {
                Color objEntity = this._colorRepository.GetById(p_id);
                if (objEntity != null)
                {
                    ColorDTO objResult = new ColorDTO
                    {
                        Id = objEntity.Id,
                        Codigo = objEntity.Codigo,
                        Descripcion = objEntity.Descripcion
                    };

                    return objResult;
                }
                else {

                    throw new Exception("No se encontro el registro");
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
