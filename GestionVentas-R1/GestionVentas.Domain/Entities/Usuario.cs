using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class Usuario : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
