using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class RecaudacionDeVentaArticuloDTO
    {
        public string ArticuloDescripcion { get; set; }
        public string CantidadVendida { get; set; }
        public string TotalRecaudado { get; set; }
    }
}
