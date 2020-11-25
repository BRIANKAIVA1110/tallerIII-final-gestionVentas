using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IPerfilService
    {
        IEnumerable<PerfilDTO> ObtenerPerfiles();
        PerfilDTO ObtenerPerfilPorId(int p_id);
    }
}