using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GestionVentas.Domain.Entities;

namespace GestionVentas.Infraestructura.Repositories
{
    public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
    {
        public VentaRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) { }
    }
}
