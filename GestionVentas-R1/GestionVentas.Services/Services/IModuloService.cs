using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IModuloService
    {
        IEnumerable<ModulosApplicacionDTO> ObtenerModulos();
        ModulosApplicacionDTO ObtenerModuloPorId(int p_id);
    }
}