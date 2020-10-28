using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IArticuloRepository:IRepositoryBase<Articulo>
    {
        
    }
}
