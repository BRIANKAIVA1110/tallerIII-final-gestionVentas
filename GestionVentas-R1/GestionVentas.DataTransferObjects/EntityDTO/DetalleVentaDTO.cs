using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public Articulo Articulo { get; set; }
        public Venta Venta { get; set; }
        public decimal CantidadUnidades { get; set; }



    }
}
