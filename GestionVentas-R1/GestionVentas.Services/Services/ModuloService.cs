using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ModuloService : IModuloService
    {
        private readonly IModuloRepository _moduloRepository;
        public ModuloService(IModuloRepository moduloRepository)
        {
            this._moduloRepository = moduloRepository;
        }

        public IEnumerable<ModulosApplicacionDTO> ObtenerModulos()
        {
            var result = this._moduloRepository.Get().ToList();
            List<ModulosApplicacionDTO> listUsuarioDTO = result.Select(x => new ModulosApplicacionDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion
            }).ToList();

            return listUsuarioDTO;
        }
        public ModulosApplicacionDTO ObtenerModuloPorId(int p_id)
        {
            var result = this._moduloRepository.GetById(p_id);

            ModulosApplicacionDTO objUsuarioDTO = new ModulosApplicacionDTO
            {
                Id = result.Id,
                Descripcion = result.Descripcion
            };
            return objUsuarioDTO;

        }
    }
}
