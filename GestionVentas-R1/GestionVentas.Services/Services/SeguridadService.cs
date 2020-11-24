using GestionVentas.Common.Encriptacion;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class SeguridadService : ISeguridadService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public SeguridadService(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        public int VerificarCredenciales(UsuarioDTO UserDTO) {

            Usuario objUsuario = this._usuarioRepository.Get().FirstOrDefault(x => x.UserName == UserDTO.UserName);
            if (objUsuario != null) {
                string password = Encriptador.Desencriptar(objUsuario.Password);
                if (UserDTO.Password == password)
                    return objUsuario.Id;
            }
            
            return 0;
        }
    }
}
