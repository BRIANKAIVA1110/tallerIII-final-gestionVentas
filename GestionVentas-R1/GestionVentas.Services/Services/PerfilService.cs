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
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly IModuloRepository _moduloRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PerfilService(IPerfilRepository perfilRepository, IModuloRepository moduloRepository, IUsuarioRepository usuarioRepository)
        {
            this._perfilRepository = perfilRepository;
            this._moduloRepository = moduloRepository;
            this._usuarioRepository = usuarioRepository;
        }


        

        public int AgregarPerfil(PerfilDTO p_perfilDTO)
        {
            try
            {
                
                var result = this._perfilRepository.AgregarPerfilConModulos(p_perfilDTO);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int ModificarPerfil(PerfilDTO p_perfilDTO)
        {
            try
            {
                Perfil objEntity = this._perfilRepository.GetById(p_perfilDTO.Id);

                objEntity.Descripcion = p_perfilDTO.Descripcion;

                int result = this._perfilRepository.Update(objEntity);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int EliminarPerfil(int p_id)
        {
            try
            {
                Perfil objEntity = this._perfilRepository.GetById(p_id);

                int result = this._perfilRepository.Delete(objEntity);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IEnumerable<PerfilDTO> getPerfiles()
        {
            try
            {
                var result = this._perfilRepository.Get()
                .Select(x => new PerfilDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion
                });

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public PerfilDTO getPerfil(int p_id)
        {
            try
            {
                Perfil objEntity = this._perfilRepository.GetById(p_id);
                if (objEntity != null)
                {
                    PerfilDTO objResult = new PerfilDTO
                    {
                        Id = objEntity.Id,
                        Descripcion = objEntity.Descripcion
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

        public byte[] GenerarExportacionRegistros()
        {
            var result = this._perfilRepository.Get()
                .Select(x => new PerfilDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion
                });
            if (result.Any())
            {
                StringBuilder sb = new StringBuilder();
                string separador = ";";
                foreach (var item in result)
                {
                    sb.AppendLine($"{item.Id}{separador}{item.Descripcion}");
                }
                byte[] byteFile = Encoding.UTF8.GetBytes(sb.ToString());

                return byteFile;
            }

            return new byte[1];
        }



    }
}
