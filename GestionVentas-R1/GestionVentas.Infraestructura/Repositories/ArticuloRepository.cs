using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ArticuloRepository:RepositoryBase<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(ApplicationContext applicationContext) : base(applicationContext) { }


        public override Articulo GetById(int p_id)
        {
            Articulo result = this._entity.Include(x => x.Modelo).Include(x => x.Color).FirstOrDefault(x=> x.Id == p_id);

            return result;
        }

        public override IEnumerable<Articulo> Get()
        {
            IEnumerable<Articulo> listResult = this._entity.Include(x => x.Modelo).Include(x => x.Color).ToList();

            return listResult;
        }
    }
}
