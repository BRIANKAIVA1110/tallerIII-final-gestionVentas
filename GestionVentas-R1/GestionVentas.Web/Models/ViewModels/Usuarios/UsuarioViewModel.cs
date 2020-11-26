using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string Password { get; set; }
        public string ConfirmacionPassword { get; set; }

        public int PerfilId { get; set; }
        public string PerfilDescripcion { get; set; }
        
        public string ModuloDescripcion { get; set; }


        public List<SelectListItem> Perfiles { get; set; }

    }
}
