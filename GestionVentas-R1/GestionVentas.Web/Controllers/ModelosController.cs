using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
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

        public ModelosController(IModeloService modeloService) {
            this._modeloService = modeloService;
        }


        public IActionResult Index() {

            var result = this._modeloService.getModelos().ToList();

            return View(result);
        }

        [HttpPost]
        public IActionResult Agregar(ModeloDTO p_modelo) {

            this._modeloService.AgregarModelo(p_modelo);

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
