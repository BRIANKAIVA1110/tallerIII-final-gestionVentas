﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Articulos
{
    public class ArticuloViewModel
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Este campo es obligatorio")]
        public string Codigo { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }
        [Required]
        public int ModeloId { get; set; }
        [Display(Name ="Modelo")]
        public string ModeloDescripcion { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Display(Name = "Color")]
        public string ColorDescripcion { get; set; }
        [Required]
        public int MarcaId { get; set; }
        [Display(Name = "Marca")]
        public string MarcaDescripcion { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Display(Name = "Categoria")]
        public string CategoriaDescripcion { get; set; }
        public List<SelectListItem> Modelos { get; set; }
        public List<SelectListItem> Colores { get; set; }
        public List<SelectListItem> Marcas { get; set; }
        public List<SelectListItem> Categorias { get; set; }

    }
}
