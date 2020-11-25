using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        public PerfilService(IPerfilRepository perfilRepository)
        {
            this._perfilRepository = perfilRepository;
        }


        public IEnumerable<PerfilDTO> ObtenerPerfiles()
        {
            var result = this._perfilRepository.Get().ToList();
            List<PerfilDTO> listUsuarioDTO = result.Select(x => new PerfilDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion
            }).ToList();

            return listUsuarioDTO;
        }
        public PerfilDTO ObtenerPerfilPorId(int p_id)
        {
            var result = this._perfilRepository.GetById(p_id);

            PerfilDTO objUsuarioDTO = new PerfilDTO
            {
                Id = result.Id,
                Descripcion = result.Descripcion
            };
            return objUsuarioDTO;
        }
    }
}
