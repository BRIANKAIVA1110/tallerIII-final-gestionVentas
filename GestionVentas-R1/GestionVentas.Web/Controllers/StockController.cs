using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Models.ViewModels.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockArticuloService _stockArticuloService;
        private readonly IMapper _mapper;

        public StockController(IStockArticuloService stockArticuloService, IMapper mapper) {
            this._stockArticuloService = stockArticuloService;
            this._mapper = mapper;
        }


        public IActionResult Index() {

            List<StockViewModel> listStockViewModelo = this._stockArticuloService.getStockArticulos()
                .Select(x => this._mapper.Map<StockViewModel>(x))
                .ToList();

            return View(listStockViewModelo);
        }

        [HttpPost]
        public IActionResult Agregar(StockViewModel p_stockArticuloVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                StockArticuloDTO stockArticuloDTO = this._mapper.Map<StockArticuloDTO>(p_stockArticuloVM);
                int result = this._stockArticuloService.AgregarStockArticulo(stockArticuloDTO);
                if (result > 0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }
        }


        public IActionResult Modificar(StockViewModel p_stockArticuloVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                StockArticuloDTO stockArticuloDTO = this._mapper.Map<StockArticuloDTO>(p_stockArticuloVM);
                int result = this._stockArticuloService.ModificarStockArticulo(stockArticuloDTO);

                if (result > 0)
                    return RedirectToAction("index");
                else
                    return View("form");
            }
        }

        public IActionResult Detalle(int Id)
        {
            StockArticuloDTO stockArticuloDTO = this._stockArticuloService.getStockArticulo((int)Id);
            if (stockArticuloDTO != null)
            {
                StockViewModel stockViewModel = this._mapper.Map<StockArticuloDTO, StockViewModel>(stockArticuloDTO);
                return View(stockViewModel);
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
                List<StockViewModel> liststockViewModel = this._stockArticuloService.getStockArticulos()
                .Where(x => x.ArticuloDescripcion.Contains(p_query))
                .Select(x => this._mapper.Map<StockViewModel>(x))
                .ToList();

                return View("index", liststockViewModel);
            }
            else
            {
                List<StockViewModel> listColorViewModel = this._stockArticuloService.getStockArticulos()
                .Select(x => this._mapper.Map<StockViewModel>(x))
                .ToList();
                return View("index", listColorViewModel);
            }


        }

        public IActionResult Eliminar(int Id)
        {

            var result = this._stockArticuloService.EliminarStockArticulo(Id);

            return RedirectToAction("index");
        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Color/Form")]
        [Route("stock/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id)
        {
            if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
            {

                ViewData["accionCRUD"] = accionCRUD;

                StockViewModel stockArticuloViewModel = new StockViewModel();
                List<SelectListItem> articulos = this._stockArticuloService.getStockArticulos()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.ArticuloDescripcion
                    }).ToList();


                if (accionCRUD.Equals(AccionesCRUD.AGREGAR)) {
                    
                    stockArticuloViewModel.Articulos = articulos;
                    return View();
                }

                if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {
                    StockArticuloDTO stockArticuloDTO = this._stockArticuloService.getStockArticulo((int)Id);
                    stockArticuloViewModel = this._mapper.Map<StockViewModel>(stockArticuloDTO);
                    return View(stockArticuloViewModel);
                }

            }
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
