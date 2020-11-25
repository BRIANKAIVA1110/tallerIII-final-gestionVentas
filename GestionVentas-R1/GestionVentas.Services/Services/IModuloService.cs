using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IModuloService
    {
        IEnumerable<ModuloDTO> ObtenerModulos();
        ModuloDTO ObtenerModuloPorId(int p_id);
    }
}