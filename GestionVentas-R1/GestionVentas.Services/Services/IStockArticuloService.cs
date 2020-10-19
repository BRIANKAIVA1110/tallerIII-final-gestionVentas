using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IStockArticuloService
    {
        int AgregarStockArticulo(StockArticuloDTO p_stockArticuloDTO);
        int ModificarStockArticulo(StockArticuloDTO p_stockArticuloDTO);
        int EliminarStockArticulo(int p_id);
        StockArticuloDTO getStockArticulo(int p_id);
        IEnumerable<StockArticuloDTO> getStockArticulos();
    }
}