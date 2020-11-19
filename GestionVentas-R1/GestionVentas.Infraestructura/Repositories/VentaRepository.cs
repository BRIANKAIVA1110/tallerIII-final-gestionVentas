using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using System.Linq;

namespace GestionVentas.Infraestructura.Repositories
{
    public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
    {
        private readonly ApplicationContext _applicationContext;

        public VentaRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) {
            this._applicationContext = applicationContext;
        }

        public int ProcesarVenta (List<CarroItemDTO> p_carroItems) {
            decimal totalFinal = 0;

            //se crea entidad venta.
            Venta objVenta = new Venta {
                FechaVenta = DateTime.Now,
                TotalFinal = p_carroItems.Sum(x => x.Precio * x.CantidadUnidades)
            };
            this._applicationContext.Set<Venta>().Add(objVenta);
            var result1 = this._applicationContext.SaveChanges();


            IEnumerable<DetalleVenta> objDetalleVenta = p_carroItems.Select(x=> new DetalleVenta { 
                Articulo = this._applicationContext.Set<Articulo>().Where(z=> z.Id == x.Id).FirstOrDefault(),
                CantidadUnidades = x.CantidadUnidades,
                Venta = objVenta,

            });

            
            this._applicationContext.Set<DetalleVenta>().AddRange(objDetalleVenta);
            var result2 = this._applicationContext.SaveChanges();

            return 1;
        }


    }
}
