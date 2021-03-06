﻿using Dapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Queries
{
    public class ObtenerRecaudacionDeVentaArticulo:IQuery<List<RecaudacionDeVentaArticuloDTO>>
    {
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public ObtenerRecaudacionDeVentaArticulo(string fechaDesde,string fechaHasta)
        {
            this.FechaDesde = fechaDesde;
            this.FechaHasta = fechaHasta;
        }
        public List<RecaudacionDeVentaArticuloDTO> Execute(IDbConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            //recaudacion de venta por articulo vendidos.
            sb.AppendLine("SELECT");
            sb.AppendLine("concat(art.codigobarras,' - ',art.descripcion,' - ', md.descripcion,' - ', colres.descripcion) as ArticuloDescripcion, ");
            sb.AppendLine("count(art.Id) as CantidadVendida,");
            sb.AppendLine("sum(v.totalfinal) as TotalRecaudado");
            sb.AppendLine("from ventas as v ");
            sb.AppendLine("inner join detalleventas as dv on v.id = dv.ventaid");
            sb.AppendLine("inner join articulos as art on dv.articuloId = art.id");
            sb.AppendLine("inner join modelos as md on art.modeloId = md.id");
            sb.AppendLine("inner join colores as colres on art.colorId = colres.id");
            sb.AppendLine("inner join marcas as mar on art.colorId = mar.id");
            sb.AppendLine("inner join categorias as cat on art.colorId = cat.id");
            sb.AppendLine($"where cast(FechaVenta as date) between '{this.FechaDesde}' and '{this.FechaHasta}'");
            sb.AppendLine("group by   ArticuloDescripcion");
            sb.AppendLine("order by CantidadVendida desc;");


            var result = connection.Query<RecaudacionDeVentaArticuloDTO>(sb.ToString()).ToList();

            return result;
        }
    }
}
