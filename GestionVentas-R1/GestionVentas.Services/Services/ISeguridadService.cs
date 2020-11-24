using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Services
{
    public interface ISeguridadService
    {
        int VerificarCredenciales(UsuarioDTO UserDTO);
    }
}
