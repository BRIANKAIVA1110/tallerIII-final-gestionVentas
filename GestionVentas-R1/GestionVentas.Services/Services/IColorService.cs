using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Services
{
    public interface IColorService
    {
        int AgregarColor(ColorDTO p_colorDTO);
        int ModificarColor(ColorDTO p_colorDTO);
        int EliminarColor(int p_id);
        ColorDTO getColor(int p_id);
        IEnumerable<ColorDTO> getColores();
    }
}
