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

            List<ColorViewModel> colorViewModels= this._colorService.getColores()
                .Select(x => this._mapper.Map<ColorViewModel>(x)).ToList();

            return View(colorViewModels);

        }

        [HttpPost]
        public IActionResult Agregar(ColorViewModel p_colorVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                ColorDTO colorDTO = this._mapper.Map<ColorDTO>(p_colorVM);
                int result = this._colorService.AgregarColor(colorDTO);
                if (result > 0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }
        }


        public IActionResult Modificar(ColorViewModel p_colorVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                ColorDTO colorDTO = this._mapper.Map<ColorDTO>(p_colorVM);
                int result = this._colorService.ModificarColor(colorDTO);

                if (result > 0)
                    return RedirectToAction("index");
                else
                    return View("form");
            }
        }

        public IActionResult Detalle(int Id)
        {
            ColorDTO colorDTO = this._colorService.getColor((int)Id);
            if (colorDTO != null)
            {
                ColorViewModel colorViewModel = this._mapper.Map<ColorDTO, ColorViewModel>(colorDTO);
                return View(colorViewModel);
            }
            else
            {
                return View("index"); //deberia mostrar un msg de notificacion
            }
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            if (p_query != null)
            {
                List<ColorViewModel> listColorViewModel = this._colorService.getColores()
                .Where(x => x.Codigo.Contains(p_query) ||
                    x.Descripcion.Contains(p_query))
                .Select(x => this._mapper.Map<ColorViewModel>(x))
                .ToList();

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
                    ColorDTO colorDTO = this._colorService.getColor((int)Id);
                    ColorViewModel colorViewModel = this._mapper.Map<ColorViewModel>(colorDTO);
                    return View(colorViewModel);
                }

            }
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
