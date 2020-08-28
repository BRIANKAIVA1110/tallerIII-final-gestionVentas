using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IColorRepository
    {
        int Add(Color p_entity);

        int Delete(Color p_entity);
        Color ExecuteQuery(DbContext context);
        IEnumerable<Color> Get();
        Color GetById(int p_id);
        int Update(Color p_entity);
    }
}
