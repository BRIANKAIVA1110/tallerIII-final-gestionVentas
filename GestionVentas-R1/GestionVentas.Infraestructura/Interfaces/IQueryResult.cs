using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;

namespace GestionVentas.Infraestructura.Interfaces
{
    public interface IQuery<T> where T:class
    {
        T Execute(IDbConnection connection);
    }
}
