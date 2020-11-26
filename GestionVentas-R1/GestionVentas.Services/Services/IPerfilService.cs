using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IPerfilService
    {
        int AgregarPerfil(PerfilDTO p_perfilDTO);
        int ModificarPerfil(PerfilDTO p_perfilDTO);
        int EliminarPerfil(int p_id);
        PerfilDTO getPerfil(int p_id);
        IEnumerable<PerfilDTO> getPerfiles();
        byte[] GenerarExportacionRegistros();
    }
}