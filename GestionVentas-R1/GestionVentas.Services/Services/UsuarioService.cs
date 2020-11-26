using GestionVentas.Common.Encriptacion;
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

        public int AgregarUsuario(UsuarioDTO p_UsuarioDTO)
        {
            try
            {
                Perfil entityPerfil = this._perfilRepository.GetById(p_UsuarioDTO.PerfilId);
                int result = this._usuarioRepository.Add(new Usuario
                {
                    UserName = p_UsuarioDTO.UserName,
                    Password = Encriptador.Encriptar(p_UsuarioDTO.Password),
                    Perfil = entityPerfil
                });

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int ModificarUsuario(UsuarioDTO p_UsuarioDTO)
        {
            try
            {
                Usuario entityUsuario = this._usuarioRepository.GetById(p_UsuarioDTO.Id);
                Perfil entityPerfil = this._perfilRepository.GetById(p_UsuarioDTO.PerfilId);

                entityUsuario.UserName = p_UsuarioDTO.UserName;
                entityUsuario.Password = Encriptador.Encriptar(p_UsuarioDTO.Password);
                entityUsuario.Perfil = entityPerfil;

                int result = this._usuarioRepository.Update(entityUsuario);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int EliminarUsuario(int p_id)
        {
            try
            {
                Usuario objEntity = this._usuarioRepository.GetById(p_id);

                int result = this._usuarioRepository.Delete(objEntity);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IEnumerable<UsuarioDTO> getUsuarios()
        {
            try
            {
                var result = this._usuarioRepository.Get()
                .Select(x => new UsuarioDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    PerfilDescripcion = x.Perfil.Descripcion,
                    ModuloDescripcion = string.Join(" - ", (this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(x.Id)).ToList()).Any() ? this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(x.Id)).Select(x => x.Descripcion).ToList() : new List<string> { })
                });

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public UsuarioDTO getUsuario(int p_id)
        {
            try
            {
                Usuario objEntity = this._usuarioRepository.GetById(p_id);
                if (objEntity != null)
                {
                    UsuarioDTO objResult = new UsuarioDTO
                    {
                        Id = objEntity.Id,
                        UserName = objEntity.UserName,
                        PerfilDescripcion = objEntity.Perfil.Descripcion,
                        ModuloDescripcion = string.Join(" - ", (this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(objEntity.Id)).ToList()).Any() ? this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(objEntity.Id)).Select(x => x.Descripcion).ToList() : new List<string> { })
                    };

                    return objResult;
                }
                else
                {

                    throw new Exception("No se encontro el registro");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

      

        //public byte[] GenerarExportacionRegistros()
        //{
        //    var result = this._colorRepository.Get()
        //        .Select(x => new UsuarioDTO
        //        {
        //            Id = x.Id,
        //            Codigo = x.Codigo,
        //            Descripcion = x.Descripcion
        //        });
        //    if (result.Any())
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        string separador = ";";
        //        foreach (var item in result)
        //        {
        //            sb.AppendLine($"{item.Codigo}{separador}{item.Descripcion}");
        //        }
        //        byte[] byteFile = Encoding.UTF8.GetBytes(sb.ToString());

        //        return byteFile;
        //    }

        //    return new byte[1];
        //}
    }
}
