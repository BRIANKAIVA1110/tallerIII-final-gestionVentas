using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Models.ViewModels.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class UsuariosController:Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IMapper _mapper;
        public UsuariosController(IUsuarioService usuarioService, IMapper mapper, IPerfilService perfilService)
        {
            this._usuarioService = usuarioService;
            this._mapper = mapper;
            this._perfilService = perfilService;

        }

        public IActionResult Index()
        {
            try
            {
                List<UsuarioViewModel> colorViewModels = this._usuarioService.getUsuarios()
                .Select(x => this._mapper.Map<UsuarioViewModel>(x)).ToList();
                return View(colorViewModels);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }


        }

        [HttpPost]
        public IActionResult Agregar(UsuarioViewModel p_UsuarioVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");               
                else
                {
                    if (p_UsuarioVM.Password != p_UsuarioVM.ConfirmacionPassword)
                        throw new Exception("No coinciden las contraseñas ingresadas, asegurese de que sean identicos.");

                    if (p_UsuarioVM.UserName.Trim().Contains(" "))
                        throw new Exception("El nombre de usuario de debe contener espacios en blando.");
                    if (p_UsuarioVM.PerfilId==0)
                        throw new Exception("Debe seleccionar un perfil para el usuario.");

                    UsuarioDTO usuarioDTO = this._mapper.Map<UsuarioDTO>(p_UsuarioVM);
                    int result = this._usuarioService.AgregarUsuario(usuarioDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<UsuarioViewModel> UsuarioViewModels = this._usuarioService.getUsuarios()
                    .Select(x => this._mapper.Map<UsuarioViewModel>(x)).ToList();
                    return View("index", UsuarioViewModels);

                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_UsuarioVM);
            }

        }


        public IActionResult Modificar(UsuarioViewModel p_UsuarioViewVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    UsuarioDTO usuarioDTO = this._mapper.Map<UsuarioDTO>(p_UsuarioViewVM);
                    int result = this._usuarioService.ModificarUsuario(usuarioDTO);
                    ViewBag.result = "Accion realizada con exito.";

                    List<UsuarioViewModel> colorViewModels = this._usuarioService.getUsuarios()
                    .Select(x => this._mapper.Map<UsuarioViewModel>(x)).ToList();
                    return View("index", colorViewModels);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_UsuarioViewVM);
            }

        }

        public IActionResult Detalle(int Id)
        {
            try
            {
                UsuarioDTO usuarioDTO = this._usuarioService.getUsuario((int)Id);
                if (usuarioDTO != null)
                {
                    UsuarioViewModel colorViewModel = this._mapper.Map<UsuarioDTO,UsuarioViewModel>(usuarioDTO);
                    return View(colorViewModel);
                }
                else
                {
                    ViewBag.error = "Ocurrio un error al intentar obtener el registro solicitado.";
                    return View("index"); //deberia mostrar un msg de notificacion
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index"); //deberia mostrar un msg de notificacion
            }

        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            try
            {
                if (p_query != null)
                {
                    List<UsuarioViewModel> listUsuarioViewModel = this._usuarioService.getUsuarios()
                    .Where(x => x.PerfilDescripcion.Contains(p_query) ||
                        x.ModuloDescripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<UsuarioViewModel>(x))
                    .ToList();

                    if (!listUsuarioViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
                    return View("index", listUsuarioViewModel);
                }
                else
                {
                    List<UsuarioViewModel> listColorViewModel = this._usuarioService.getUsuarios()
                    .Select(x => this._mapper.Map<UsuarioViewModel>(x))
                    .ToList();
                    return View("index", listColorViewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }



        }

        public IActionResult Eliminar(int Id)
        {
            try
            {
                var result = this._usuarioService.EliminarUsuario(Id);

                ViewBag.result = "Accion realizada con exito.";

                List<UsuarioViewModel> UsuarioViewModels = this._usuarioService.getUsuarios()
                .Select(x => this._mapper.Map<UsuarioViewModel>(x)).ToList();
                return View("index", UsuarioViewModels);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<UsuarioViewModel> UsuarioViewModels = this._usuarioService.getUsuarios()
                .Select(x => this._mapper.Map<UsuarioViewModel>(x)).ToList();
                return View("index", UsuarioViewModels);
            }

        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Color/Form")]
        [Route("Usuarios/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id)
        {
            try
            {
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {
                    List<SelectListItem> Perfiles = this._perfilService.getPerfiles().Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Descripcion} - [{x.ModulosDescripcion}]"
                    }).ToList();


                    ViewData["accionCRUD"] = accionCRUD;
                    if (accionCRUD.Equals(AccionesCRUD.AGREGAR)) {
                        UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
                        usuarioViewModel.Perfiles = Perfiles;
                        return View(usuarioViewModel);
                    }

                    if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                    {
                        UsuarioDTO usuarioDTO = this._usuarioService.getUsuario((int)Id);
                        UsuarioViewModel usuarioViewModel = this._mapper.Map<UsuarioViewModel>(usuarioDTO);
                        usuarioViewModel.Perfiles = Perfiles;

                        return View(usuarioViewModel);
                    }

                }

                throw new Exception("Ocurrio un error inesperado.");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }

        }


        //public IActionResult ExportarRegistros()
        //{
        //    try
        //    {
        //        byte[] file = this._usuarioService.GenerarExportacionRegistros();

        //        FileContentResult File = new FileContentResult(file, "application/CSV")
        //        {
        //            FileDownloadName = $"colores_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
        //        };
        //        return File;
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewBag.error = ex.Message;
        //        return View("index");
        //    }



        //}

    }

    
}
