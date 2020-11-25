using GestionVentas.Infraestructura.Repositories;
using GestionVentas.Services.Services;
using GestionVentas.Web.Models.ViewModels.Reportes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class ReporteController :Controller
    {
        private readonly IReporteService _reporteService;
        public ReporteController(IReporteService reporteService)
        {
            this._reporteService = reporteService;
        }


      
        public IActionResult ReporteRecaudacionArticulosVendidos() {
            return View();
        }
        
        public IActionResult ReporteCantidadArticuloVendidosPordia()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GenerarReporteRecaudacionArticulosVendidos(ReporteBetweenFechaViewModel p_reporteBetweenFechaViewModel) {

            string fechaDesde = p_reporteBetweenFechaViewModel.FechaDesde.ToString("yyyy-MM-dd");
            string fechaHasta = p_reporteBetweenFechaViewModel.FechaHasta.ToString("yyyy-MM-dd");

            byte[] fileByte = this._reporteService.GenerarReporteRecaudacionDeVentaArticuloVendidos(fechaDesde, fechaHasta);

            FileContentResult file = new FileContentResult(fileByte, "text/plain");
            file.FileDownloadName = $"ReporteRecaudacionArticulosVendidos_{fechaDesde}_{fechaHasta}.txt";

            return file;
        }
        [HttpPost]
        public IActionResult GenerarReporteCantidadArticuloVendidosPordia(ReporteBetweenFechaViewModel p_reporteBetweenFechaViewModel)
        {
            string fechaDesde = p_reporteBetweenFechaViewModel.FechaDesde.ToString("yyyy-MM-dd");
            string fechaHasta = p_reporteBetweenFechaViewModel.FechaHasta.ToString("yyyy-MM-dd");

            byte[] fileByte = this._reporteService.GenerarReporteCantidadVentasDeArticulosVendidos(fechaDesde, fechaHasta);

            FileContentResult file = new FileContentResult(fileByte, "text/plain");
            file.FileDownloadName = $"ReporteCantidadArticuloVendidosPordia_{fechaDesde}_{fechaHasta}.txt";

            return file;
        }
    }
}
