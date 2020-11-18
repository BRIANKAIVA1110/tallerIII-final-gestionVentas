using GestionVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Domain.Entities
{
    public class Articulo: IEntity
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descripcion { get; set; }
        public Modelo Modelo { get; set; }
        public Color Color {get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria {get; set; }
    }
}
