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
    public class ModelosController : Controller
    {
        private readonly IModeloService _modeloService;
        private readonly IMapper _mapper;

        public ModelosController(IMapper mapper,IModeloService modeloService) {
            this._modeloService = modeloService;
            this._mapper = mapper;
        }


        public IActionResult Index() {
            try
            {
                List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                .ToList();
                return View(listmodeloViewModel);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }
            

            
        }

        [HttpPost]
        public IActionResult Agregar(ModeloViewModel p_modeloVM) {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    ModeloDTO modeloDTO = this._mapper.Map<ModeloViewModel, ModeloDTO>(p_modeloVM);
                    int result = this._modeloService.AgregarModelo(modeloDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                   .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                   .ToList();
                    return View("index", listmodeloViewModel);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_modeloVM);
            }
           
        }
        public IActionResult Modificar(ModeloViewModel p_modeloVM) {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    ModeloDTO modeloDTO = this._mapper.Map<ModeloViewModel, ModeloDTO>(p_modeloVM);
                    int result = this._modeloService.ModificarModelo(modeloDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                   .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                   .ToList();
                    return View("index", listmodeloViewModel);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_modeloVM);
            }
            
            
        }
        public IActionResult Detalle(int Id) {
            try
            {
                ModeloDTO modeloDTO = this._modeloService.getModelo((int)Id);
                if (modeloDTO != null)
                {
                    ModeloViewModel modeloViewModel = this._mapper.Map<ModeloDTO, ModeloViewModel>(modeloDTO);
                    return View(modeloViewModel);
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
                return View("index");
            }
            
            
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            try
            {
                //TODO: ver diferencias: contains vs like method
                if (p_query != null)
                {
                    List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                    .Where(x => x.Codigo.Contains(p_query) ||
                        x.Descripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                    .ToList();

                    if (!listmodeloViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
                    return View("index", listmodeloViewModel);
                }
                else
                {
                    List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                    .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                    .ToList();
                    return View("index", listmodeloViewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }
            
           
           
        }

        public IActionResult Eliminar(int Id) {
            try
            {
                var result = this._modeloService.EliminarModelo(Id);

                ViewBag.result = "Accion realizada con exito.";

                List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                    .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                    .ToList();
                return View("index", listmodeloViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message}. El registro debe tener una referencia con otro.";
                List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                    .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                    .ToList();

                return View("index", listmodeloViewModel);
            }
            
        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Modelos/Form")]
        [Route("Modelos/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id) {

            try
            {
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {

                    ViewData["accionCRUD"] = accionCRUD;
                    if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                        return View();

                    if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                    {
                        ModeloDTO modeloDTO = this._modeloService.getModelo((int)Id);
                        ModeloViewModel modeloViewModel = this._mapper.Map<ModeloViewModel>(modeloDTO);
                        return View(modeloViewModel);
                    }

                }

                throw new Exception("Ocurrio un error inesperado.");
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View();
            }
            
            
        }


        public IActionResult ExportarRegistros() {
            try
            {
                byte[] file = this._modeloService.GenerarExportacionRegistros();

                FileContentResult File = new FileContentResult(file, "application/CSV")
                {
                    FileDownloadName = $"modelos_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
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
