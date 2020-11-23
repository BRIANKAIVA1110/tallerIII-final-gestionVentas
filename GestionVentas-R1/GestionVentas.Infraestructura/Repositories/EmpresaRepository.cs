using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) { }
    }
   
}
