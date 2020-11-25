using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class Modulo: IEntity
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public ICollection<ModulosXPerfil> ModulosxPerfil { get; set; }
    }
}
