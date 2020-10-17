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

            List<MarcaViewModel> marcaViewModel = this._marcaService.getMarcas()
                .Select(x => this._mapper.Map<MarcaViewModel>(x)).ToList();

            return View(marcaViewModel);

        }

        [HttpPost]
        public IActionResult Agregar(MarcaViewModel p_marcaVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                MarcaDTO marcaDTO = this._mapper.Map<MarcaDTO>(p_marcaVM);
                int result = this._marcaService.AgregarMarca(marcaDTO);
                if (result > 0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }
        }


        public IActionResult Modificar(MarcaViewModel p_marcaVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                MarcaDTO marcaDTO = this._mapper.Map<MarcaDTO>(p_marcaVM);
                int result = this._marcaService.ModificarMarca(marcaDTO);

                if (result > 0)
                    return RedirectToAction("index");
                else
                    return View("form");
            }
        }

        public IActionResult Detalle(int Id)
        {
            MarcaDTO marcaDTO = this._marcaService.getMarca((int)Id);
            if (marcaDTO != null)
            {
                MarcaViewModel marcaViewModel = this._mapper.Map<MarcaViewModel>(marcaDTO);
                return View(marcaViewModel);
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
                List<MarcaViewModel> listmarcaViewModel = this._marcaService.getMarcas()
                .Where(x => x.Codigo.Contains(p_query) ||
                    x.Descripcion.Contains(p_query))
                .Select(x => this._mapper.Map<MarcaViewModel>(x))
                .ToList();

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

        public IActionResult Eliminar(int Id)
        {

            var result = this._marcaService.getMarca(Id);

            return RedirectToAction("index");
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
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
