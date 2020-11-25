using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class ModulosXPerfil: IEntity
    {
        public int Id{ get; set; }
        public Perfil Perfil{ get; set; }
        public Modulo Modulo{ get; set; }
    }
}
