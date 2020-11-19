using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class Venta: IEntity
    {
        public int Id { get; set; }
        public decimal TotalFinal { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}
