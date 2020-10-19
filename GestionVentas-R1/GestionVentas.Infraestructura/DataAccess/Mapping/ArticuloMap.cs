using System;
using System.Collections.Generic;
using System.Text;
using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySQL.Data.EntityFrameworkCore;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
            builder.ToTable("Articulos");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired();
            builder.HasOne(x => x.Modelo).WithMany().HasForeignKey("modeloId");
            builder.HasOne(x => x.Color).WithMany().HasForeignKey("colorId");
            builder.HasOne(x => x.Marca).WithMany().HasForeignKey("marcaId");
            builder.HasOne(x => x.Categoria).WithMany().HasForeignKey("categoriaId");

        }
    }
}
