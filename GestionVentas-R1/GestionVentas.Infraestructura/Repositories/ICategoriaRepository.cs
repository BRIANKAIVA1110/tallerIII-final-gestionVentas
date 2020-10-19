using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface ICategoriaRepository
    {
        int Add(Categoria p_entity);

        int Delete(Categoria p_entity);
        Categoria ExecuteQuery(DbContext context);
        IEnumerable<Categoria> Get();
        Categoria GetById(int p_id);
        int Update(Categoria p_entity);
    }
}
