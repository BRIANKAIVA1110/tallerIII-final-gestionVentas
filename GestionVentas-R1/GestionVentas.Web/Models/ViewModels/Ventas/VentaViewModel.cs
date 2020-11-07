using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Ventas
{
    public class VentaViewModel
    {
        public List<ArticuloDTO> CarroArticulos { get; set; }
    }
}
