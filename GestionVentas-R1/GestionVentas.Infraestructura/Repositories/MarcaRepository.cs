using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class MarcaRepository : RepositoryBase<Marca>, IMarcaRepository
    {

        public MarcaRepository(ApplicationContext applicationContex, IDbConnection connection) : base(applicationContex, connection) { }
    }
}
