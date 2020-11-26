using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GestionVentas.Infraestructura.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationContext _applicationContext;
        public UsuarioRepository(ApplicationContext applicationContext, IDbConnection dbConnection):base(applicationContext,dbConnection)
        {
            this._applicationContext = applicationContext;
        }
        public override IEnumerable<Usuario> Get()
        {
            IEnumerable<Usuario> listUsuario = this._entity.Include(x => x.Perfil);
                
            
            return listUsuario;
        }
        public override Usuario GetById(int p_id)
        {
            Usuario entity = this._entity.Include(x=> x.Perfil).FirstOrDefault(x => x.Id == p_id);

            return entity;
        }


        public IEnumerable<ModulosApplicacion> pepe() {

            List<ModulosApplicacion> result = this._applicationContext.Set<ModulosApplicacion>().ToList();
            return result;
        }
    }
}
