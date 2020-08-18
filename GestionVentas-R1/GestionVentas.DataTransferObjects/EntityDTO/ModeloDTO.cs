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
        [StringLength(10, ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [MinLength(10, ErrorMessage = "ESTE CAMPO DEBE CONTENER 10 CARACTERES")]
        public string Codigo { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "ESTE CAMPO ES REQUERIDO")]
        [MinLength(2, ErrorMessage = "ESTE CAMPO DEBE CONTENER 2 CARACTERES COMO MINIMO")]
        public string Descripcion { get; set; }

        //[Required]
        //[StringLength(8)]
        //[MinLength(8)]
        //[RegularExpression("^[0-9]{8}$")]
        //public string DNI { get; set; }
    }
}
