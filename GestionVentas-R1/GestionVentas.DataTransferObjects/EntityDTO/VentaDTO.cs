using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public decimal TotalFinal { get; set; }
        public DateTime FechaVenta { get; set; }

        public string NroComporbante { get; set; }
    }
}
