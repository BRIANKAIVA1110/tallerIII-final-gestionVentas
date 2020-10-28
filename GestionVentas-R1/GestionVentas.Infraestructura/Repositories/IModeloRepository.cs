using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IModeloRepository:IRepositoryBase<Modelo>
    {
        
    }
}