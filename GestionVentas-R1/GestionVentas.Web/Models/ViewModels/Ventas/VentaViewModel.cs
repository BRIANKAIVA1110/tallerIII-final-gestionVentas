using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Ventas
{
    public class VentaViewModel
    {
        public int Id { get; set; }
        public string FechaVenta { get; set; }
        public string NroComporbante { get; set; }
        public string TotalFinal { get; set; }
    }


    

   
}
