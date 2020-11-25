using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioDTO> ObtenerUsuarios();
        UsuarioDTO ObtenerUsuarioPorId(int p_id);
    }
}