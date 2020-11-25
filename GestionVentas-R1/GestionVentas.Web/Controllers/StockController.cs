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
            try
            {
                List<StockViewModel> listStockViewModelo = this._stockArticuloService.getStockArticulos()
                .Select(x => this._mapper.Map<StockViewModel>(x))
                .ToList();

                return View(listStockViewModelo);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Agregar(StockViewModel p_stockArticuloVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("error");
                else
                {
                    StockArticuloDTO stockArticuloDTO = this._mapper.Map<StockArticuloDTO>(p_stockArticuloVM);
                    int result = this._stockArticuloService.AgregarStockArticulo(stockArticuloDTO);

                    ViewBag.result = "Accion realizada con exito.";
                    List<StockViewModel> listStockViewModelo = this._stockArticuloService.getStockArticulos()
                    .Select(x => this._mapper.Map<StockViewModel>(x))
                    .ToList();

                    return View("index", listStockViewModelo);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_stockArticuloVM);
            }

        }


        public IActionResult Modificar(StockViewModel p_stockArticuloVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("error");
                else
                {
                    StockArticuloDTO stockArticuloDTO = this._mapper.Map<StockArticuloDTO>(p_stockArticuloVM);
                    int result = this._stockArticuloService.ModificarStockArticulo(stockArticuloDTO);

                    ViewBag.result = "Accion realizada con exito.";
                    List<StockViewModel> listStockViewModelo = this._stockArticuloService.getStockArticulos()
                    .Select(x => this._mapper.Map<StockViewModel>(x))
                    .ToList();

                    return View("index", listStockViewModelo);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_stockArticuloVM);
            }

        }

        public IActionResult Detalle(int Id)
        {
            try
            {
                StockArticuloDTO stockArticuloDTO = this._stockArticuloService.getStockArticulo((int)Id);
                if (stockArticuloDTO != null)
                {
                    StockViewModel stockViewModel = this._mapper.Map<StockArticuloDTO, StockViewModel>(stockArticuloDTO);
                    return View(stockViewModel);
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
                //ver diferencias: contains vs like method
                if (p_query != null)
                {
                    List<StockViewModel> liststockViewModel = this._stockArticuloService.getStockArticulos()
                    .Where(x => x.ArticuloDescripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<StockViewModel>(x))
                    .ToList();

                    if (!liststockViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
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
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }



        }

        public IActionResult Eliminar(int Id)
        {
            try
            {
                var result = this._stockArticuloService.EliminarStockArticulo(Id);

                ViewBag.result = "Accion realizada con exito.";
                List<StockViewModel> listColorViewModel = this._stockArticuloService.getStockArticulos()
                    .Select(x => this._mapper.Map<StockViewModel>(x))
                    .ToList();
                return View("index", listColorViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<StockViewModel> listColorViewModel = this._stockArticuloService.getStockArticulos()
                    .Select(x => this._mapper.Map<StockViewModel>(x))
                    .ToList();
                return View("index", listColorViewModel);
            }
            
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
            try
            {
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {

                    ViewData["accionCRUD"] = accionCRUD;

                    StockViewModel stockArticuloViewModel = new StockViewModel();


                    /*
                     * obtiene los articulos sin asignacion de stock
                     */
                    List<SelectListItem> articulosSinAsignacionStock = this._stockArticuloService.ObtenerArticulosSinAsignacionStock()
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = $"{x.ModeloDescripcion} - {x.ColorDescripcion} - {x.MarcaDescripcion} - {x.CategoriaDescripcion}"
                        }).ToList();


                    if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                    {

                        stockArticuloViewModel.Articulos = articulosSinAsignacionStock;
                        return View(stockArticuloViewModel);
                    }

                    if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                    {
                        StockArticuloDTO stockArticuloDTO = this._stockArticuloService.getStockArticulo((int)Id);
                        stockArticuloViewModel = this._mapper.Map<StockViewModel>(stockArticuloDTO);
                        return View(stockArticuloViewModel);
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

        public IActionResult ExportarRegistros()
        {
            try
            {
                byte[] file = this._stockArticuloService.GenerarExportacionRegistros();

                FileContentResult File = new FileContentResult(file, "application/CSV")
                {
                    FileDownloadName = $"stockArticulos_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
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
