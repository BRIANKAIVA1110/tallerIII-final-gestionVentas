using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Interfaces
{
    public interface IQueryResult
    {
        IEnumerable<object> Execute(DbContext context);
    }
}
