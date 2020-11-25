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
        public string Perfil { get; set; }
        public string PermisosModulos { get; set; }

    }
}
