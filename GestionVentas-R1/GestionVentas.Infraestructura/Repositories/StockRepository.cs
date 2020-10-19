using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class StockArticuloRepository:RepositoryBase<StockArticulo>, IStockArticuloRepository
    {
        public StockArticuloRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public override StockArticulo GetById(int p_id)
        {
            StockArticulo stockArticulo = this._entity.Include(x => x.Articulo)
                .Include(x=> x.Articulo.Modelo)
                .Include(x => x.Articulo.Marca)
                .Include(x => x.Articulo.Color)
                .Include(x => x.Articulo.Categoria)
                .FirstOrDefault(x => x.Id == p_id);

            return stockArticulo;
        }

        public override IEnumerable<StockArticulo> Get()
        {
            IEnumerable<StockArticulo> listStockArticulo = this._entity.Include(x => x.Articulo)
                .Include(x => x.Articulo.Color)
                .Include(x => x.Articulo.Modelo);

            return listStockArticulo;
        }

    }
}
