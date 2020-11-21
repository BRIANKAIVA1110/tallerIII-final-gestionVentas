using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using GestionVentas.Infraestructura.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        private readonly ApplicationContext _applicationContext;
        public readonly DbSet<T> _entity;
        private readonly IDbConnection _connection;
        public RepositoryBase(ApplicationContext applicationContext, IDbConnection connection)
        {
            this._applicationContext = applicationContext;
            this._entity = applicationContext.Set<T>();
            this._connection = connection;
        }


        public virtual int Add(T p_entity)
        {
            try
            {
                this._entity.Add(p_entity);
                var result = this._applicationContext.SaveChanges();

                return result;
            }
            catch (DbUpdateException ex) {
                throw ex;
            }
           
        }
        public virtual int Update(T p_entity)
        {
            try
            {
                this._entity.Update(p_entity);
                var result = this._applicationContext.SaveChanges();

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }
        public virtual int Delete(T p_entity)
        {
            try
            {
                this._entity.Remove(p_entity);
                var result = this._applicationContext.SaveChanges();

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }
        public virtual IEnumerable<T> Get()
        {
            try
            {
                IEnumerable<T> result = this._entity.ToList();

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public virtual T GetById(int p_id)
        {
            try
            {
                T result = this._entity.FirstOrDefault(x => x.Id == p_id);

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

       

        public W ExecuteQuery<W>(IQuery<W> p_query) where W : class
        {
            return p_query.Execute(this._connection);
        }
    }
}
