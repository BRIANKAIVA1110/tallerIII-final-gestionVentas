using GestionVentas.DataTransferObjects.EntityDTO;
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

        public IActionResult Index() {
            var result = this._modeloService.get();
            return View();
        }

        public IActionResult Form([FromQuery] string accionCrud = AccionesCRUD.AGREGAR) {
            ViewData["accionCRUD"] = accionCrud;

            return View();
        }
        [HttpPost]
        public IActionResult AgregarModelo([FromForm] ModeloDTO modelo) {
            return RedirectToAction("index");
        }
    }
}
