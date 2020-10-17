using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IMarcaRepository
    {
        int Add(Marca p_entity);
        int Delete(Marca p_entity);
        Marca ExecuteQuery(DbContext context);
        IEnumerable<Marca> Get();
        Marca GetById(int p_id);
        int Update(Marca p_entity);
    }
}
