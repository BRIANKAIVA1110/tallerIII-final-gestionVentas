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
    public class AutenticacionController:Controller
    {
        private readonly ISeguridadService _seguridadService;
        private readonly IMapper _mapper;
        public AutenticacionController(ISeguridadService seguridadService, IMapper mapper) {
            this._seguridadService = seguridadService;
            this._mapper = mapper;

        }


        public IActionResult IniciarSesion() {

            return View();
        }
        public IActionResult CerrarSession()
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "usuario", 0);
            return RedirectToAction("IniciarSesion");
        }
        [HttpPost]
        public IActionResult VerificarCredenciales(AutenticacionViewModel p_autenticacionViewModel) {

            UsuarioDTO objUsuarioDTO = this._mapper.Map<UsuarioDTO>(p_autenticacionViewModel);
            int userId = this._seguridadService.VerificarCredenciales(objUsuarioDTO);
            if (userId != 0) {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "usuario", userId.ToString());
                return RedirectToAction("Index", "home");
            }else{
                ViewBag.error = "Usuario o contraseña invalido";
                return View("IniciarSesion");
            }
            
        }
    }
}
