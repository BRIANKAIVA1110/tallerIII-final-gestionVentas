using GestionVentas.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IReporteRepository
    {
        W ExecuteQuery<W>(IQuery<W> p_query) where W : class;
    }
}
