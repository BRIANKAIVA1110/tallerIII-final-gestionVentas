using GestionVentas.Infraestructura.DataAccess.Queries;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            this._reporteRepository = reporteRepository;
        }


        public byte[] GenerarReporteCantidadVentasDeArticulosVendidos(string fechaDesde, string fechaHasta)
        {

            var result = this._reporteRepository.ExecuteQuery(new ObtenerCantidadVentasDeArticulo(fechaDesde, fechaHasta));

            StringBuilder sb = new StringBuilder();
            string separador = "";
            for (int i = 0; i < 120; i++)
                separador += "*";

            sb.AppendFormat("{0}\n\n", separador);
            sb.AppendFormat("{0}\n\n", $"**Reporte : Cantidad articulo Vendidos por dia entre {fechaDesde} - {fechaHasta}**");
            sb.AppendFormat("{0}\n\n", separador);
            sb.AppendFormat("|{0,-70}|{1,-20}|{2,-30}|\n", "Articulo Descripcion", "Fecha Venta", "Cantidad Vendida");
            sb.AppendFormat("{0}\n\n", separador);
            foreach (var item in result)
            {
                sb.AppendFormat("|{0,-70}|{1,-20}|{2,-30}|\n", $"{item.ArticuloDescripcion}", $"{item.FechaVenta}", $"{item.CantidadVendida}");
            }
            sb.AppendFormat("{0}\n", separador);
            sb.AppendFormat("{0,110}\n", $"Cantidad Total Vendida: {result.Sum(x => int.Parse(x.CantidadVendida))}");
            sb.AppendFormat("{0}\n", separador);

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
        public byte[] GenerarReporteRecaudacionDeVentaArticuloVendidos(string fechaDesde, string fechaHasta)
        {

            var result = this._reporteRepository.ExecuteQuery(new ObtenerRecaudacionDeVentaArticulo(fechaDesde, fechaHasta));

            StringBuilder sb = new StringBuilder();
            string separador = "";
            for (int i = 0; i < 120; i++)
                separador += "*";

            sb.AppendFormat("{0}\n\n", separador);
            sb.AppendFormat("|{0}\n\n", $"**Reporte : Recaudacion articulos Vendidos entre {fechaDesde} - {fechaHasta}**");
            sb.AppendFormat("{0}\n\n", separador);
            sb.AppendFormat("|{0,-70}|{1,-20}|$ {2,-30}|\n", "Articulo Descripcion", "Cantidad Vendida", "Total Recaudacion");
            sb.AppendFormat("{0}\n", separador);
            foreach (var item in result)
            {
                sb.AppendFormat("|{0,-70}|{1,-20}|{2,-30}|\n", $"{item.ArticuloDescripcion}", $"{item.CantidadVendida}", $"{item.TotalRecaudado}");
            }
            sb.AppendFormat("{0}\n", separador);
            decimal totalRecaudado = result.Sum(x => Convert.ToDecimal(x.TotalRecaudado,new CultureInfo("en-us")));
            sb.AppendFormat("|{0,120}|\n", $"Recaudacion Total Vendida: $ {totalRecaudado}");
            sb.AppendFormat("{0}\n", separador);

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
