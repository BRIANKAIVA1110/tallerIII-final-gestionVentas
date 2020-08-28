using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Services
{
    public interface IArticuloService
    {
        int AgregarArticulo(ArticuloDTO p_articuloDTO);
        int ModificarArticulo(ArticuloDTO p_articuloDTO);
        int EliminarArticulo(int p_id);
        ArticuloDTO getArticulo(int p_id);
        IEnumerable<ArticuloDTO> getArticulos();
    }
}
