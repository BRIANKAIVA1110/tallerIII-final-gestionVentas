using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace GestionVentas.Web.Models.ViewModels.Articulos
{
    public class ModeloViewModel
    {

        public int? Id { get; set; }
        [Required]
        [MinLength(length: 1, ErrorMessage = "Debe ingresar datos para este campo")]
        [MaxLength(length: 4, ErrorMessage = "Debe ingresar un codigo entre de 4 caracteres")]
        public string Codigo { get; set; }
        [Required]
        [MinLength(length: 1,ErrorMessage = "Debe ingresar datos para este campo")]
        [MaxLength(length:250,ErrorMessage = "Debe ingresar una descripcion entre 1 y 250 caracteres")]
        public string Descripcion { get; set; }
    }
}
