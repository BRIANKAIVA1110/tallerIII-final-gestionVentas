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
    public class MarcasController: Controller
    {
        private readonly IMarcaService _marcaService;
        private readonly IMapper _mapper;
        public MarcasController(IMarcaService marcaService, IMapper mapper) {
            this._marcaService = marcaService;
            this._mapper = mapper;
        }


        public IActionResult Index()
        {
            try
            {
                List<MarcaViewModel> marcaViewModel = this._marcaService.getMarcas()
                .Select(x => this._mapper.Map<MarcaViewModel>(x)).ToList();

                return View(marcaViewModel);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }
            

        }

        [HttpPost]
        public IActionResult Agregar(MarcaViewModel p_marcaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    MarcaDTO marcaDTO = this._mapper.Map<MarcaDTO>(p_marcaVM);
                    int result = this._marcaService.AgregarMarca(marcaDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<MarcaViewModel> marcaViewModel = this._marcaService.getMarcas()
                     .Select(x => this._mapper.Map<MarcaViewModel>(x)).ToList();

                    return View("index",marcaViewModel);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_marcaVM);
            }
            
        }


        public IActionResult Modificar(MarcaViewModel p_marcaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    MarcaDTO marcaDTO = this._mapper.Map<MarcaDTO>(p_marcaVM);
                    int result = this._marcaService.ModificarMarca(marcaDTO);
                    ViewBag.result = "Accion realizada con exito.";

                    List<MarcaViewModel> marcaViewModel = this._marcaService.getMarcas()
                     .Select(x => this._mapper.Map<MarcaViewModel>(x)).ToList();
                    return View("index",marcaViewModel);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_marcaVM);
            }
            
        }

        public IActionResult Detalle(int Id)
        {
            try
            {
                MarcaDTO marcaDTO = this._marcaService.getMarca((int)Id);
                if (marcaDTO != null)
                {
                    MarcaViewModel marcaViewModel = this._mapper.Map<MarcaViewModel>(marcaDTO);
                    return View(marcaViewModel);
                }
                else
                {
                    ViewBag.error = "Ocurrio un erro al intentar obtener el registro solicitado.";
                    return View("index"); 
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index"); 
            }

        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            try
            {
                if (p_query != null)
                {
                    List<MarcaViewModel> listmarcaViewModel = this._marcaService.getMarcas()
                    .Where(x => x.Codigo.Contains(p_query) ||
                        x.Descripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<MarcaViewModel>(x))
                    .ToList();

                    if (!listmarcaViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
                    return View("index", listmarcaViewModel);
                }
                else
                {
                    List<MarcaViewModel> listmarcaViewModel = this._marcaService.getMarcas()
                    .Select(x => this._mapper.Map<MarcaViewModel>(x))
                    .ToList();
                    return View("index", listmarcaViewModel);
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
                var result = this._marcaService.EliminarMarca(Id);

                ViewBag.result = "Accion realizada con exito.";
                List<MarcaViewModel> listmarcaViewModel = this._marcaService.getMarcas()
                    .Select(x => this._mapper.Map<MarcaViewModel>(x))
                    .ToList();
                return View("index", listmarcaViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<MarcaViewModel> listmarcaViewModel = this._marcaService.getMarcas()
                    .Select(x => this._mapper.Map<MarcaViewModel>(x))
                    .ToList();
                return View("index", listmarcaViewModel);
            }

        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("marca/Form")]
        [Route("marca/Form/{Id?}")]
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
                        MarcaDTO marcaDTO = this._marcaService.getMarca((int)Id);
                        MarcaViewModel marcaViewModel = this._mapper.Map<MarcaViewModel>(marcaDTO);
                        return View(marcaViewModel);
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
                byte[] file = this._marcaService.GenerarExportacionRegistros();

                FileContentResult File = new FileContentResult(file, "application/CSV")
                {
                    FileDownloadName = $"marxas_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
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
