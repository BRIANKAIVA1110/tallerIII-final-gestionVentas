using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.DataAccess.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class StockArticuloRepository:RepositoryBase<StockArticulo>, IStockArticuloRepository
    {
        public StockArticuloRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) { }

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
