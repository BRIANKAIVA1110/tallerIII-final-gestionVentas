using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ArticuloRepository:RepositoryBase<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
