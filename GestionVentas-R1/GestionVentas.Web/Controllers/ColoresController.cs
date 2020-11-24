using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Models.ViewModels.Articulos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class ColoresController:Controller
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public ColoresController(IColorService colorService, IMapper mapper) {
            this._colorService = colorService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<ColorViewModel> colorViewModels = this._colorService.getColores()
                .Select(x => this._mapper.Map<ColorViewModel>(x)).ToList();
                return View(colorViewModels);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }
            

        }

        [HttpPost]
        public IActionResult Agregar(ColorViewModel p_colorVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    ColorDTO colorDTO = this._mapper.Map<ColorDTO>(p_colorVM);
                    int result = this._colorService.AgregarColor(colorDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<ColorViewModel> colorViewModels = this._colorService.getColores()
                    .Select(x => this._mapper.Map<ColorViewModel>(x)).ToList();
                    return View("index", colorViewModels);

                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_colorVM);
            }
           
        }


        public IActionResult Modificar(ColorViewModel p_colorVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    ColorDTO colorDTO = this._mapper.Map<ColorDTO>(p_colorVM);
                    int result = this._colorService.ModificarColor(colorDTO);
                    ViewBag.result = "Accion realizada con exito.";

                    List<ColorViewModel> colorViewModels = this._colorService.getColores()
                    .Select(x => this._mapper.Map<ColorViewModel>(x)).ToList();
                    return View("index", colorViewModels);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_colorVM);
            }
            
        }

        public IActionResult Detalle(int Id)
        {
            try
            {
                ColorDTO colorDTO = this._colorService.getColor((int)Id);
                if (colorDTO != null)
                {
                    ColorViewModel colorViewModel = this._mapper.Map<ColorDTO, ColorViewModel>(colorDTO);
                    return View(colorViewModel);
                }
                else {
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
                    List<ColorViewModel> listColorViewModel = this._colorService.getColores()
                    .Where(x => x.Codigo.Contains(p_query) ||
                        x.Descripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<ColorViewModel>(x))
                    .ToList();

                    if (!listColorViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
                    return View("index", listColorViewModel);
                }
                else
                {
                    List<ColorViewModel> listColorViewModel = this._colorService.getColores()
                    .Select(x => this._mapper.Map<ColorViewModel>(x))
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
                var result = this._colorService.EliminarColor(Id);

                ViewBag.result = "Accion realizada con exito.";

                List<ColorViewModel> colorViewModels = this._colorService.getColores()
                .Select(x => this._mapper.Map<ColorViewModel>(x)).ToList();
                return View("index", colorViewModels);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<ColorViewModel> colorViewModels = this._colorService.getColores()
                .Select(x => this._mapper.Map<ColorViewModel>(x)).ToList();
                return View("index", colorViewModels);
                return View("index");
            }
            
        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Color/Form")]
        [Route("Color/Form/{Id?}")]
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
                        ColorDTO colorDTO = this._colorService.getColor((int)Id);
                        ColorViewModel colorViewModel = this._mapper.Map<ColorViewModel>(colorDTO);
                        return View(colorViewModel);
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


        public IActionResult ExportarRegistros()
        {
            try
            {
                byte[] file = this._colorService.GenerarExportacionRegistros();

                FileContentResult File = new FileContentResult(file, "application/CSV")
                {
                    FileDownloadName = $"colores_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
                };
                return File;
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View("index");
            }



        }
    }
}
