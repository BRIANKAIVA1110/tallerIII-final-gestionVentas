using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface ICategoriaService
    {
        int AgregarCategoria(CategoriaDTO p_CategoriaDTO);
        int EliminarCategoria(int p_id);
        CategoriaDTO getCategoria(int p_id);
        IEnumerable<CategoriaDTO> getCategorias();
        int ModificarCategoria(CategoriaDTO p_CategoriaDTO);
        byte[] GenerarExportacionRegistros();
    }
}