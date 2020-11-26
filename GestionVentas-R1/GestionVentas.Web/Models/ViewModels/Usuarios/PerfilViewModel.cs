using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Usuarios
{
    public class PerfilViewModel
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public string ModulosDescripcion { get; set; }

        public bool IsCheckArticulos { get; set; }
        public bool IsCheckStock { get; set; }
        public bool IsCheckVentas { get; set; }
        public bool IsCheckReportes { get; set; }
        public bool IsCheckSeguridad { get; set; }

    }

    
}
