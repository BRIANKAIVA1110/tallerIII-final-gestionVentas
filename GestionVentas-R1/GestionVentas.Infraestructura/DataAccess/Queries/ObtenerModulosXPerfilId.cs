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
    public class ObtenerModulosXPerfilId:IQuery<List<ModuloDTO>>
    {
        public int PerfilId { get; set; }
        public ObtenerModulosXPerfilId(int p_perfilId)
        {
            this.PerfilId = p_perfilId;
        }

        public List<ModuloDTO> Execute(IDbConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            //recaudacion de venta por articulo vendidos.
            sb.AppendLine("SELECT");
            sb.AppendLine("mdl.Id as Id,");
            sb.AppendLine("per.Id as PerfilId,");
            sb.AppendLine("mdl.descripcion as Descripcion");
            sb.AppendLine("from perfiles as per left join modulosxperfiles as mxp on per.id = mxp.perfilid");
            sb.AppendLine("inner join modulos as mdl on mxp.moduloId = mdl.id");
            sb.AppendLine($"where per.id = {this.PerfilId};");

            var result = connection.Query<ModuloDTO>(sb.ToString()).ToList();

            return result;
        }
    }
}
