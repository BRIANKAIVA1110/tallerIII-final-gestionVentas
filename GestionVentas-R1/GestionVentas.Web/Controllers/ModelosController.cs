using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Models.ViewModels.Modelos;
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

            var result = this._modeloService.getModelos().ToList();
            var resultToView = this._mapper.Map<ModeloViewModel>(result);
            return View(resultToView);
        }

        [HttpPost]
        public IActionResult Agregar(ModeloViewModel p_modelo) {

            var objDTO = this._mapper.Map<ModeloDTO>(p_modelo);
            this._modeloService.AgregarModelo(objDTO);

            return RedirectToAction("index");
        }
        public IActionResult Modificar(ModeloDTO p_modelo) {

            this._modeloService.ModificarModelo(p_modelo);

            return RedirectToAction("index");
        }
        public IActionResult Detalle(int Id) {

            ModeloDTO objResult = this._modeloService.getModelo((int)Id);
            return View(objResult);
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            var result = this._modeloService.getModelos()
                .Where(x => x.Codigo.Contains(p_query) || x.Descripcion.Contains(p_query))
                .ToList();

            return View("index", result);
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
                    ModeloDTO objResult = this._modeloService.getModelo((int)Id);
                    return View(objResult);
                }

            }
            return RedirectToAction("ERROR", "HOME");
        }
        
    }
}
