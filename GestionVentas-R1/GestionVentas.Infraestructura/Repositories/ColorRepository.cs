using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GestionVentas.Domain.Entities;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) { }
    }
}
