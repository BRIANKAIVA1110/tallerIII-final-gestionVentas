using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class ArticuloDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int ModeloId { get; set; }
        public string ModeloDescripcion { get; set; }
        public int ColorId { get; set; }
        public string ColorDescripcion { get; set; }

       
    }
}
