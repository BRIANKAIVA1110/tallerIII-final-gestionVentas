using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class CarroItemDTO:ArticuloDTO
    {
        public int CantidadUnidades { get; set; }
        public decimal Total { get; set; }
    }
}
