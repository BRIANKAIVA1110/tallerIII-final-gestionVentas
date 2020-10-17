using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IStockArticuloService
    {
        IEnumerable<StockArticuloDTO> obtenerStockArticulos();
    }
}