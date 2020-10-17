using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class StockArticuloDTO
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string ArticuloDescripcion { get; set; }
        public decimal Cantidad { get; set; }
    }
}
