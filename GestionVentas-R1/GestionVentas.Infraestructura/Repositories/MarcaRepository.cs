using GestionVentas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class MarcaRepository : RepositoryBase<Marca>, IMarcaRepository
    {

        public MarcaRepository(ApplicationContext applicationContex) : base(applicationContex) { }
    }
}
