using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IModeloRepository
    {
        int Add(Modelo p_entity);

        int Delete(Modelo p_entity);
        Modelo ExecuteQuery(DbContext context);
        IEnumerable<Modelo> Get();
        Modelo GetById(int p_id);
        int Update(Modelo p_entity);
    }
}