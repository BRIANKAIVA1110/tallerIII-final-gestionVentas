using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class StockArticulo : IEntity
    {
        public int Id { get; set; }
        public Articulo Articulo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
