using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ModuloRepository: IModuloRepository
    {
        private readonly ApplicationContext _applicationContext;
        public readonly DbSet<ModulosApplicacion> _entity;
        private readonly IDbConnection _connection;
        public ModuloRepository(ApplicationContext applicationContext, IDbConnection connection)
        {
            this._applicationContext = applicationContext;
            this._entity = applicationContext.Set<ModulosApplicacion>();
            this._connection = connection;
        }


        public virtual int Add(ModulosApplicacion p_entity)
        {
            try
            {
                this._entity.Add(p_entity);
                var result = this._applicationContext.SaveChanges();

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }
        public virtual int Update(ModulosApplicacion p_entity)
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
        public virtual int Delete(ModulosApplicacion p_entity)
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
        public virtual IEnumerable<ModulosApplicacion> Get()
        {
            try
            {
                IEnumerable<ModulosApplicacion> result = this._entity.ToList();

                return result;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public virtual ModulosApplicacion GetById(int p_id)
        {
            try
            {
                ModulosApplicacion result = this._entity.FirstOrDefault(x => x.Id == p_id);

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
