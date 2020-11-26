using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Filters;
using GestionVentas.Web.Models.ViewModels.Usuarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    [VerificarAutorizacionModulo(ModulosAplicacionEnum.seguridad)]
    public class PerfilesController:Controller
    {
        private readonly IPerfilService _perfilService;
        private readonly IMapper _mapper;
        public PerfilesController(IPerfilService perfilService, IMapper mapper)
        {
            this._perfilService = perfilService;
            this._mapper = mapper;
        }


        public IActionResult Index()
        {
            try
            {
                List<PerfilViewModel> perfilViewModels = this._perfilService.getPerfiles()
                .Select(x => this._mapper.Map<PerfilViewModel>(x)).ToList();
                return View(perfilViewModels);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }


        }

        [HttpPost]
        public IActionResult Agregar(PerfilViewModel p_perfilVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    PerfilDTO objPerfilDTO = this._mapper.Map<PerfilDTO>(p_perfilVM);
                    int result = this._perfilService.AgregarPerfil(objPerfilDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<PerfilViewModel> perfilViewModel = this._perfilService.getPerfiles()
                    .Select(x => this._mapper.Map<PerfilViewModel>(x)).ToList();
                    return View("index", perfilViewModel);

                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_perfilVM);
            }

        }


        public IActionResult Modificar(PerfilViewModel p_perfilVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    PerfilDTO objPerfilDTO = this._mapper.Map<PerfilDTO>(p_perfilVM);
                    int result = this._perfilService.ModificarPerfil(objPerfilDTO);
                    ViewBag.result = "Accion realizada con exito.";

                    List<PerfilViewModel> colorViewModels = this._perfilService.getPerfiles()
                    .Select(x => this._mapper.Map<PerfilViewModel>(x)).ToList();
                    return View("index", colorViewModels);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException?.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_perfilVM);
            }

        }

        public IActionResult Detalle(int Id)
        {
            try
            {
                PerfilDTO objPerfilDTO = this._perfilService.getPerfil((int)Id);
                if (objPerfilDTO != null)
                {
                    PerfilViewModel perfilViewModel = this._mapper.Map<PerfilViewModel>(objPerfilDTO);
                    return View(perfilViewModel);
                }
                else
                {
                    ViewBag.error = "Ocurrio un erro al intentar obtener el registro solicitado.";
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
                    List<PerfilViewModel> listperfilViewModel = this._perfilService.getPerfiles()
                    .Where(x=> x.Descripcion.Contains(p_query)||
                         x.ModulosDescripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<PerfilViewModel>(x))
                    .ToList();

                    if (!listperfilViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
                    return View("index", listperfilViewModel);
                }
                else
                {
                    List<PerfilViewModel> listperfilViewModel = this._perfilService.getPerfiles()
                    .Select(x => this._mapper.Map<PerfilViewModel>(x))
                    .ToList();
                    return View("index", listperfilViewModel);
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
                var result = this._perfilService.EliminarPerfil(Id);

                ViewBag.result = "Accion realizada con exito.";

                List<PerfilViewModel> perfilViewModel = this._perfilService.getPerfiles()
                .Select(x => this._mapper.Map<PerfilViewModel>(x)).ToList();
                return View("index", perfilViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<PerfilViewModel> perfilViewModel = this._perfilService.getPerfiles()
                .Select(x => this._mapper.Map<PerfilViewModel>(x)).ToList();
                return View("index", perfilViewModel);
            }

        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Color/Form")]
        [Route("Perfiles/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id)
        {
            try
            {
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {

                    ViewData["accionCRUD"] = accionCRUD;
                    if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                        return View();

                    if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                    {
                        PerfilDTO objPerfilDTO = this._perfilService.getPerfil((int)Id);
                        PerfilViewModel perfilViewModel = this._mapper.Map<PerfilViewModel>(objPerfilDTO);
                        return View(perfilViewModel);
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
        //        byte[] file = this._perfilService.GenerarExportacionRegistros();

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
