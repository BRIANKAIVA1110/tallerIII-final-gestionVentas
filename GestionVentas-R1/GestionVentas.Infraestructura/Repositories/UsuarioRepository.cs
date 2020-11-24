using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext applicationContext, IDbConnection dbConnection):base(applicationContext,dbConnection)
        {

        }
    }
}
