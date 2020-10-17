using AutoMapper;
using GestionVentas.Services.Services;
using GestionVentas.Web.Models.ViewModels.Stock;
using Microsoft.AspNetCore.Mvc;
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

            List<StockViewModel> listStockViewModelo = this._stockArticuloService.obtenerStockArticulos()
                .Select(x => this._mapper.Map<StockViewModel>(x))
                .ToList();

            return View(listStockViewModelo);
        }
    }
}
