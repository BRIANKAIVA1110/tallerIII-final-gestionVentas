using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Stock
{
    public class StockViewModel
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string ArticuloDescripcion { get; set; }
        public decimal Cantidad { get; set; }
    }
}
