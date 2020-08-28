using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IArticuloRepository
    {
        int Add(Articulo p_entity);

        int Delete(Articulo p_entity);
        Articulo ExecuteQuery(DbContext context);
        IEnumerable<Articulo> Get();
        Articulo GetById(int p_id);
        int Update(Articulo p_entity);
    }
}
