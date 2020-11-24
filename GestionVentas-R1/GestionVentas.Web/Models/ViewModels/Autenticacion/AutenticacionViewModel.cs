using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Autenticacion
{
    public class AutenticacionViewModel
    {
        public int UsuarioId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
