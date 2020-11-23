using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public  class Empresa:IEntity
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Cuit { get; set; }
        public string Domicilio { get; set; }
    }
}
