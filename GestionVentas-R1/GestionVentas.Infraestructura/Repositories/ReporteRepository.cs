using GestionVentas.Infraestructura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        
        private readonly IDbConnection _connection;
        public ReporteRepository( IDbConnection connection)
        {
            this._connection = connection;
        }

        public W ExecuteQuery<W>(IQuery<W> p_query) where W : class
        {
            return p_query.Execute(this._connection);
        }
    }
}
