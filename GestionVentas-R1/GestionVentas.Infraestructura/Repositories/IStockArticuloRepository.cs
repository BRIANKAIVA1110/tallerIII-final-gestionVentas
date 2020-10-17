using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IStockArticuloRepository
    {
        int Add(StockArticulo p_entity);
        int Delete(StockArticulo p_entity);
        StockArticulo ExecuteQuery(DbContext context);
        IEnumerable<StockArticulo> Get();
        StockArticulo GetById(int p_id);
        int Update(StockArticulo p_entity);
    }
}
