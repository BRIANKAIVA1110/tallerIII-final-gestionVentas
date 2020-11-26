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
    public class ObtenerModulosApplicacion : IQuery<List<ModulosApplicacionDTO>>
    {
        public List<ModulosApplicacionDTO> Execute(IDbConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            //recaudacion de venta por articulo vendidos.
            sb.AppendLine("SELECT");
            sb.AppendLine("*");
            sb.AppendLine("from ModulosApplicacion  ");
            
            var result = connection.Query<ModulosApplicacionDTO>(sb.ToString()).ToList();

            return result;
        }
    }
}
