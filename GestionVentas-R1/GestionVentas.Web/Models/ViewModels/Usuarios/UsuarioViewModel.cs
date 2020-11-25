using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Usuarios
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public int PerfilId { get; set; }
        public string PerfilDescripcion { get; set; }
        
        public string ModuloDescripcion { get; set; }

    }
}
