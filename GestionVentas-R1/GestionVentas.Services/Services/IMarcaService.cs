using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Services
{
    public interface IMarcaService
    {
        int AgregarMarca(MarcaDTO p_marcaDTO);
        int ModificarMarca(MarcaDTO p_marcaDTO);
        int EliminarMarca(int p_id);
        MarcaDTO getMarca(int p_id);
        IEnumerable<MarcaDTO> getMarcas();

        byte[] GenerarExportacionRegistros();
    }
}
