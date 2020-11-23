using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class ClienteDTO
    {
        public string NombreCompleto { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public string CodiPostal { get; set; }
        public string Telefono { get; set; }
        
    }
}
