using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionVentas.DataTransferObjects.EntityDTO
{
    public class ArticuloDTO
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descripcion { get; set; }
        public int ModeloId { get; set; }
        public string ModeloDescripcion { get; set; }
        public int ColorId { get; set; }
        public string ColorDescripcion { get; set; }
        public int MarcaId { get; set; }
        public string MarcaDescripcion { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaDescripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal CantidadStock { get; set; }


    }
}
