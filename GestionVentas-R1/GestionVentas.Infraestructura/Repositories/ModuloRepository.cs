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
    public class ModuloRepository : RepositoryBase<Modulo>, IModuloRepository
    {
        public ModuloRepository(ApplicationContext applicationContext, IDbConnection dbconnection) : base(applicationContext, dbconnection)
        {

        }


        public override IEnumerable<Modulo> Get()
        {
            var result = this._entity.Include(x => x.ModulosxPerfil).ToList();

            return result;
        }

        public override Modulo GetById(int p_id)
        {
            var result = this._entity.Include(x => x.ModulosxPerfil).FirstOrDefault(x => x.Id == p_id);

            return result;
        }
    }
}
