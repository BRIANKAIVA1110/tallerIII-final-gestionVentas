using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class CategoriaRepository:RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) { }
    }
}
