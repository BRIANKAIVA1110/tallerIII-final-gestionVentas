using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class Modelo : IEntity
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        
    }
}
