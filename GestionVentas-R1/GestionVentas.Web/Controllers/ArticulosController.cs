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
    public class ArticulosController:Controller
    {
        private readonly IArticuloService _articuloService;
        public ArticulosController(IArticuloService articuloService) 
        {
            this._articuloService = articuloService;
        }

        public IActionResult Index()
        {

            var result = this._articuloService.getArticulos().ToList();

            return View(result);
        }

        [HttpPost]
        public IActionResult Agregar(ArticuloDTO p_articuloDTO)
        {

            this._articuloService.AgregarArticulo(p_articuloDTO);

            return RedirectToAction("index");
        }
        public IActionResult Modificar(ArticuloDTO p_articuloDTO)
        {

            this._articuloService.ModificarArticulo(p_articuloDTO);

            return RedirectToAction("index");
        }
        public IActionResult Detalle(int Id)
        {

            ArticuloDTO objResult = this._articuloService.getArticulo((int)Id);
            return View(objResult);
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            var result = this._articuloService.getArticulos()
                .Where(x => x.Codigo.Contains(p_query) || x.Descripcion.Contains(p_query))
                .ToList();

            return View("index", result);
        }

        public IActionResult Eliminar(int Id)
        {

            var result = this._articuloService.EliminarArticulo(Id);

            return RedirectToAction("index");
        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Articulos/Form")]
        [Route("Articulos/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id)
        {
            if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
            {

                ViewData["accionCRUD"] = accionCRUD;
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                    return View();

                if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {
                    ArticuloDTO objResult = this._articuloService.getArticulo((int)Id);
                    return View(objResult);
                }

            }
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
