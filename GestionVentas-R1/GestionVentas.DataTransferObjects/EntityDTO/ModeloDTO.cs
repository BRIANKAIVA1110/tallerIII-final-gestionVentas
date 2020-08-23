using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class ModeloDTO
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [MinLength(4, ErrorMessage = "ESTE CAMPO DEBE CONTENER 4 CARACTERES")]
        public string Codigo { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [MinLength(2, ErrorMessage = "ESTE CAMPO DEBE CONTENER 2 CARACTERES COMO MINIMO")]
        public string Descripcion { get; set; }

        //[Required]
        //[StringLength(8)]
        //[MinLength(8)]
        //[RegularExpression("^[0-9]{8}$")]
        //public string DNI { get; set; }
    }
}
