using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Helper;
using GestionVentas.Web.Models.ViewModels.Autenticacion;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class SeguridadController:Controller
    {
        private readonly ISeguridadService _seguridadService;
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IMapper _mapper;
        public SeguridadController(ISeguridadService seguridadService, IMapper mapper, IPerfilService perfilService, IUsuarioService usuarioService) {
            this._seguridadService = seguridadService;
            this._mapper = mapper;
            this._perfilService = perfilService;
            this._usuarioService = usuarioService;

        }


        public IActionResult IniciarSesion() {

            return View();
        }
        public IActionResult CerrarSession()
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "usuario", null);
            return RedirectToAction("IniciarSesion");
        }
        [HttpPost]
        public IActionResult VerificarCredenciales(AutenticacionViewModel p_autenticacionViewModel) {

            UsuarioDTO objUsuarioDTO = this._mapper.Map<UsuarioDTO>(p_autenticacionViewModel);
            int userId = this._seguridadService.VerificarCredenciales(objUsuarioDTO);
            if (userId != 0) {
                //tremos los modulos por perfil apara luego ser usados en el masterPage

                List<ModulosApplicacionDTO> modulos = this._usuarioService.ObtenerModulosApplicacionSegunPerfilUsuario(userId);
                
                SessionHelper.SetObjectAsJson(HttpContext.Session, "usuario", new { UserId=userId, NombreUsuario= objUsuarioDTO.UserName, ModulosAutorizado=string.Join(";", modulos.Select(x=> x.Descripcion)) });
                return RedirectToAction("Index", "home");
            }else{
                ViewBag.error = "Usuario o contraseña invalido";
                return View("IniciarSesion");
            }
            
        }
    }
}
