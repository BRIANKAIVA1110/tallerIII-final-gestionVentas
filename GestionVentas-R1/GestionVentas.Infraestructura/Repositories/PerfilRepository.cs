using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class PerfilRepository : RepositoryBase<Perfil>, IPerfilRepository
    {
        public PerfilRepository(ApplicationContext applicationContext, IDbConnection dbConnection) : base(applicationContext, dbConnection)
        {

        }

        public override IEnumerable<Perfil> Get()
        {
            var result = this._entity.Include(x => x.ModulosxPerfil).ToList();

            return result;
        }

        public override Perfil GetById(int p_id)
        {
            var result = this._entity.Include(x => x.ModulosxPerfil).FirstOrDefault(x => x.Id == p_id);

            return result;
        }
    }
}
