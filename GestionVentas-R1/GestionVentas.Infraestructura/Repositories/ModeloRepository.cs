using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ModeloRepository : RepositoryBase<Modelo>, IModeloRepository
    {
        public ModeloRepository(ApplicationContext _applicationContext, IDbConnection connection) : base(_applicationContext, connection) { }

    }
}
