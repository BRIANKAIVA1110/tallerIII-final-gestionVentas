﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int PerfilId { get; set; }
        public string PerfilDescripcion { get; set; }

        public string ModuloDescripcion { get; set; }


    }
}
