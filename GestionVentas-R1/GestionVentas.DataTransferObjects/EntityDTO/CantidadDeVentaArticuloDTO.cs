using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class CantidadDeVentaArticuloDTO
    {
        public string ArticuloDescripcion { get; set; }
        public string FechaVenta { get; set; }
        public string CantidadVendida { get; set; }
    }
}
