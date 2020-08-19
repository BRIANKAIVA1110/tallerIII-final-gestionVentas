using GestionVentas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        private readonly ApplicationContext _applicationContext;
        private readonly DbSet<T> _entity;
        public RepositoryBase(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
            this._entity = applicationContext.Set<T>();
        }


        public virtual int Add(T p_entity)
        {
            this._entity.Add(p_entity);
            var result = this._applicationContext.SaveChanges();

            return result;
        }
        public virtual int Update(T p_entity)
        {
            this._entity.Update(p_entity);
            var result = this._applicationContext.SaveChanges();

            return result;
        }
        public virtual int Delete(T p_entity)
        {
            this._entity.Remove(p_entity);
            var result = this._applicationContext.SaveChanges();

            return result;
        }
        public virtual IEnumerable<T> Get()
        {

            IEnumerable<T> result = this._entity.ToList();

            return result;
        }

        public virtual T GetById(int p_id)
        {
            T result = this._entity.FirstOrDefault(x => x.Id == p_id);

            return result;
        }

        public virtual T ExecuteQuery(DbContext context)
        {
            throw new NotImplementedException();
        }

       
    }
}
