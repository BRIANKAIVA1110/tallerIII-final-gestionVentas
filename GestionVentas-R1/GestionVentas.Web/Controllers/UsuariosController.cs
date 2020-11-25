using GestionVentas.Services.Services;
using GestionVentas.Web.Models.ViewModels.Usuarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class UsuariosController:Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;

        }

        public IActionResult Index() {

            List<UsuarioViewModel> listUsuarioViewModel = this._usuarioService.ObtenerUsuarios().Select(
                x => new UsuarioViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    PerfilId = x.PerfilId,
                    PerfilDescripcion = x.PerfilDescripcion,
                    ModuloDescripcion = x.ModuloDescripcion,
                }).ToList();

            return View(listUsuarioViewModel);
        }
        
    }

    
}
