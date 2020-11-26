using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class PerfilDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ModulosDescripcion { get; set; }



        public bool IsCheckArticulos { get; set; }
        public bool IsCheckStock { get; set; }
        public bool IsCheckVentas { get; set; }
        public bool IsCheckReportes { get; set; }
        public bool IsCheckSeguridad { get; set; }
    }
}
