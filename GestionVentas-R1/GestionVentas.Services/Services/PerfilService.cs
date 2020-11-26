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
                int result = this._perfilRepository.ModificarPerfilConModulos(p_perfilDTO);

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
                string[] aux = new string[1];
                aux[0] = "";
                var result = this._perfilRepository.Get()
                .Select(x => new PerfilDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    ModulosDescripcion = string.Join(" - ", (this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(x.Id)).ToList()).Any()? this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(x.Id)).Select(x=> x.Descripcion).ToList(): new List<string> { })

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
                List<ModulosApplicacionDTO> listModulosApplicacionDTO = this._perfilRepository.ExecuteQuery(new ObtenerModulosXPerfilId(objEntity.Id));

                if (objEntity != null)
                {
                    PerfilDTO objResult = new PerfilDTO
                    {
                        Id = objEntity.Id,
                        Descripcion = objEntity.Descripcion
                    };

                    objResult.IsCheckArticulos = ConteaintMudulo(listModulosApplicacionDTO, "articulos");
                    objResult.IsCheckStock = ConteaintMudulo(listModulosApplicacionDTO, "stock"); ;
                    objResult.IsCheckVentas = ConteaintMudulo(listModulosApplicacionDTO, "ventas");
                    objResult.IsCheckReportes = ConteaintMudulo(listModulosApplicacionDTO, "reportes");
                    objResult.IsCheckSeguridad = ConteaintMudulo(listModulosApplicacionDTO, "seguridad");

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


        private bool ConteaintMudulo(List<ModulosApplicacionDTO> listModulosApplicacionDTO, string value) {
            bool result = listModulosApplicacionDTO.Where(x => x.Descripcion == value).FirstOrDefault()!=null? true:false;

            return result;
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
