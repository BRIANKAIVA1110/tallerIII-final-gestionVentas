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

        public IEnumerable<ModuloDTO> ObtenerModulos()
        {
            var result = this._moduloRepository.Get().ToList();
            List<ModuloDTO> listUsuarioDTO = result.Select(x => new ModuloDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion
            }).ToList();

            return listUsuarioDTO;
        }
        public ModuloDTO ObtenerModuloPorId(int p_id)
        {
            var result = this._moduloRepository.GetById(p_id);

            ModuloDTO objUsuarioDTO = new ModuloDTO
            {
                Id = result.Id,
                Descripcion = result.Descripcion
            };
            return objUsuarioDTO;

        }
    }
}
