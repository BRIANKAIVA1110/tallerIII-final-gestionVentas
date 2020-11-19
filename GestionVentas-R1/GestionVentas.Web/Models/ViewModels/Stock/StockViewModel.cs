using GestionVentas.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Stock
{
    public class StockViewModel
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string ArticuloDescripcion { get; set; }
        [IsDecimal]
        public string Cantidad { get; set; }

        public List<SelectListItem> Articulos { get; set; }
    }
}
