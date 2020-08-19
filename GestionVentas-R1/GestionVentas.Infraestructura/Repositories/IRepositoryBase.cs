using GestionVentas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IRepositoryBase<T> where T : class, IEntity
    {
        int Add(T p_entity);
        int Delete(T p_entity);
        T ExecuteQuery(DbContext context);
        IEnumerable<T> Get();
        T GetById(int p_id);
        int Update(T p_entity);
    }
}