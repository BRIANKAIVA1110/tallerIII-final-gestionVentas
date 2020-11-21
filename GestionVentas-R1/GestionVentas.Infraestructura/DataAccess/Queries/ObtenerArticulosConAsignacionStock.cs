using Dapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Queries
{
    public class ObtenerArticulosConAsignacionStock : IQuery<ArticuloDTO>
    {
        private string CodigoBarras { get; set; }
        public ObtenerArticulosConAsignacionStock(string p_codigoBarras) {
            this.CodigoBarras = p_codigoBarras;
        }

        public ArticuloDTO Execute(IDbConnection connection)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine("art.* ,");
            sb.AppendLine("CONCAT( md.codigo,' - ',md.descripcion) as ModeloDescripcion,");
            sb.AppendLine("CONCAT( col.codigo,' - ',col.descripcion) as ColorDescripcion,");
            sb.AppendLine("CONCAT( mar.codigo,' - ',mar.descripcion) as MarcaDescripcion,");
            sb.AppendLine("CONCAT( cat.codigo,' - ',cat.descripcion) as CategoriaDescripcion,");
            sb.AppendLine("stkart.cantidad as CantidadStock");
            sb.AppendLine("from articulos as art ");
            sb.AppendLine("left  join stockArticulos as stkart on art.id = stkart.articuloid");
            sb.AppendLine("inner join modelos as md on art.modeloId = md.id");
            sb.AppendLine("inner join colores as col on art.colorId = col.Id");
            sb.AppendLine("inner join Marcas as mar on art.marcaId = mar.Id");
            sb.AppendLine("inner join categorias as cat on art.categoriaId = cat.Id");
            sb.AppendLine($"where stkart.id is not null and art.codigobarras = {this.CodigoBarras} limit 1");


            var result = connection.Query<ArticuloDTO>(sb.ToString()).FirstOrDefault();

            return result;
        }
    }
}
