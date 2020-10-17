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

            List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                .ToList();

            return View(listmodeloViewModel);
        }

        [HttpPost]
        public IActionResult Agregar(ModeloViewModel p_modeloVM) {

            if (!ModelState.IsValid)
                return View("error");
            else
            {
                ModeloDTO modeloDTO = this._mapper.Map<ModeloViewModel, ModeloDTO>(p_modeloVM);
                int result = this._modeloService.AgregarModelo(modeloDTO);
                if (result > 0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }
        }
        public IActionResult Modificar(ModeloViewModel p_modeloVM) {

            if (!ModelState.IsValid)
                return View("error");
            else {
                ModeloDTO modeloDTO = this._mapper.Map<ModeloViewModel, ModeloDTO>(p_modeloVM);
                int result = this._modeloService.ModificarModelo(modeloDTO);

                if (result > 0)
                    return RedirectToAction("index");
                else
                    return View("form");
            }
            
        }
        public IActionResult Detalle(int Id) {
            
            ModeloDTO modeloDTO = this._modeloService.getModelo((int)Id);
            if (modeloDTO != null)
            {
                ModeloViewModel modeloViewModel = this._mapper.Map<ModeloDTO, ModeloViewModel>(modeloDTO);
                return View(modeloViewModel);
            }
            else {
                return View("index"); //deberia mostrar un msg de notificacion
            }
            
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            if (p_query != null)
            {
                List<ModeloViewModel> listmodeloViewModel = this._modeloService.getModelos()
                .Where(x => x.Codigo.Contains(p_query) ||
                    x.Descripcion.Contains(p_query))
                .Select(x => this._mapper.Map<ModeloDTO, ModeloViewModel>(x))
                .ToList();

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

        public IActionResult Eliminar(int Id) {

            var result = this._modeloService.EliminarModelo(Id);

            return RedirectToAction("index");
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
            if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR)) {

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
            return RedirectToAction("ERROR", "HOME");
        }
        
    }
}
