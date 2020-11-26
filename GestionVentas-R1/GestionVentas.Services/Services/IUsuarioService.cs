using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IUsuarioService
    {
        int AgregarUsuario(UsuarioDTO p_UsuarioDTO);
        int ModificarUsuario(UsuarioDTO p_UsuarioDTO);
        int EliminarUsuario(int p_id);
        UsuarioDTO getUsuario(int p_id);
        IEnumerable<UsuarioDTO> getUsuarios();
        List<ModulosApplicacionDTO> ObtenerModulosApplicacionSegunPerfilUsuario(int p_userId);
        //byte[] GenerarExportacionRegistros();
    }
}