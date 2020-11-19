using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class DetalleVentaMap : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("DetalleVentas");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CantidadUnidades).IsRequired();
            builder.HasOne(x => x.Articulo).WithMany().HasForeignKey("articuloId");
            builder.HasOne(x => x.Venta).WithMany().HasForeignKey("ventaId");
        }
    }
}
