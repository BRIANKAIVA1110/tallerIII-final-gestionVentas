using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace GestionVentas.Infraestructura.Repositories
{
    public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
    {
        private readonly ApplicationContext _applicationContext;

        public VentaRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) {
            this._applicationContext = applicationContext;
        }

        public int ProcesarVenta (List<CarroItemDTO> p_carroItems) {
            //este no es la mejor manera de hacerlo, deberia ir en la capa de servicio la logica...
            //ver como implementar patron unitOfWork.
            int result = 0;
            using (IDbContextTransaction transaction = this._applicationContext.Database.BeginTransaction()) {
                try
                {
                    decimal totalFinal = 0;

                    //se crea entidad venta.
                    Venta objVenta = new Venta
                    {
                        FechaVenta = DateTime.Now,
                        TotalFinal = p_carroItems.Sum(x => x.Precio * x.CantidadUnidades)
                    };

                    //se hace registro de la venta
                    this._applicationContext.Set<Venta>().Add(objVenta);
                    result = this._applicationContext.SaveChanges();


                    //se generan objetos del tipo detalleVenta
                    IEnumerable<DetalleVenta> objDetalleVenta = p_carroItems.Select(x => new DetalleVenta
                    {
                        Articulo = this._applicationContext.Set<Articulo>().Where(z => z.Id == x.Id).FirstOrDefault(),
                        CantidadUnidades = x.CantidadUnidades,
                        Venta = objVenta,

                    });

                    
                    //se hace registro de los detalles de la venta
                    this._applicationContext.Set<DetalleVenta>().AddRange(objDetalleVenta);
                    result = this._applicationContext.SaveChanges();

                    //se descuenta el estock de los articulos del carro de compras
                    foreach (var item in p_carroItems)
                    {
                        StockArticulo StockArticulo = this._applicationContext.Set<StockArticulo>().Include(x => x.Articulo).Where(x => x.Articulo.Id == item.Id).FirstOrDefault();
                        StockArticulo.Cantidad -= (decimal)item.CantidadUnidades;
                        this._applicationContext.Set<StockArticulo>().Update(StockArticulo);
                        result = this._applicationContext.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.ToString());
                }
            
            }
               

            return result;
        }


    }
}
