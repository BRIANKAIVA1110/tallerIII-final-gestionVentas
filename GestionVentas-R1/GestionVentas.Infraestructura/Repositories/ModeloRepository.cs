using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ModeloRepository : RepositoryBase<Modelo>, IModeloRepository
    {
        public ModeloRepository(ApplicationContext _applicationContext) : base(_applicationContext) { }

    }
}
