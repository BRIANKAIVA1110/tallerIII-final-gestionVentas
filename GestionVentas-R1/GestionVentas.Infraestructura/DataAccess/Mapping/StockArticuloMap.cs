using System;
using System.Collections.Generic;
using System.Text;
using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class StockArticuloMap : IEntityTypeConfiguration<StockArticulo>
    {
        public void Configure(EntityTypeBuilder<StockArticulo> builder)
        {
            builder.ToTable("stockArticulo");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Cantidad).IsRequired();
            builder.HasOne(x => x.Articulo).WithMany().HasForeignKey("ArticuloId");
        }
    }
}
