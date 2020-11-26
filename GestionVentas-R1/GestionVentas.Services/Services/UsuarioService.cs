using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.DataAccess.Queries;
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
            try
            {
                List<Usuario> listUsuarios = this._usuarioRepository.Get().ToList();


                //TODO: ver como traer todo por entityFramework... many to many
                foreach (var item in listUsuarios)
                {
                    item.Modulos = this._usuarioRepository.ExecuteQuery(new ObtenerModulosXPerfilId(item.Perfil.Id))
                        .Select(x=> new ModulosApplicacion { 
                            Id = x.Id,
                            Descripcion = x.Descripcion
                        })
                        .ToList();
                }

                if (listUsuarios != null) {
                    List<UsuarioDTO> listUsuarioDTO = listUsuarios.Select(x => new UsuarioDTO
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        PerfilId = x.Perfil.Id,
                        PerfilDescripcion = x.Perfil.Descripcion,
                        ModuloDescripcion = string.Join(" - ", x.Modulos.Select(x => x.Descripcion).ToList<string>())
                    }).ToList();

                    return listUsuarioDTO;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
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
