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
    public class ColoresController:Controller
    {
        private readonly IColorService _colorService;

        public ColoresController(IColorService colorService) {
            this._colorService = colorService;
        }

        public IActionResult Index()
        {

            var result = this._colorService.getColores().ToList();

            return View(result);

        }

        [HttpPost]
        public IActionResult Agregar(ColorDTO p_color)
        {

            this._colorService.AgregarColor(p_color);

            return RedirectToAction("index");
        }


        public IActionResult Modificar(ColorDTO p_color)
        {

            this._colorService.ModificarColor(p_color);

            return RedirectToAction("index");
        }

        public IActionResult Detalle(int Id)
        {

            ColorDTO objResult = this._colorService.getColor((int)Id);
            return View(objResult);
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            var result = this._colorService.getColores()
                .Where(x => x.Codigo.Contains(p_query) || x.Descripcion.Contains(p_query))
                .ToList();

            return View("index", result);
        }

        public IActionResult Eliminar(int Id)
        {

            var result = this._colorService.EliminarColor(Id);

            return RedirectToAction("index");
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
            if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
            {

                ViewData["accionCRUD"] = accionCRUD;
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                    return View();

                if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {
                    ColorDTO objResult = this._colorService.getColor((int)Id);
                    return View(objResult);
                }

            }
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
