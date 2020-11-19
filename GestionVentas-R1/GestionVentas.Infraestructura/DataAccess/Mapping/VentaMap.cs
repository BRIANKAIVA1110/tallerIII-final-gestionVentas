using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class VentaMap : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Ventas");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TotalFinal).IsRequired();
            builder.Property(x => x.FechaVenta).IsRequired();
        }
    }
}
