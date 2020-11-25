using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            this._usuarioRepository = usuarioRepository;
            this._perfilRepository = perfilRepository;
        }


        public IEnumerable<UsuarioDTO> ObtenerUsuarios()
        {
            var result = this._usuarioRepository.Get().ToList();
            List<UsuarioDTO> listUsuarioDTO = result.Select(x => new UsuarioDTO
            {
                Id = x.Id,
                UserName = x.UserName,
                PerfilId = x.Perfil.Id,
                PerfilDescripcion = "asdaskdjaskldjaslkd",
            }).ToList();

            return listUsuarioDTO;
        }
        public UsuarioDTO ObtenerUsuarioPorId(int p_id) {
            var result = this._usuarioRepository.GetById(p_id);

            UsuarioDTO objUsuarioDTO = new UsuarioDTO {
                Id = result.Id,
                UserName = result.UserName
            };
            return objUsuarioDTO;

        }
    }
}
