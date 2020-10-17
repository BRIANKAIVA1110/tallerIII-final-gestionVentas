using System;
using System.Collections.Generic;
using System.Text;
using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class MarcaMap : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("Marcas");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired();
        }
    }
}
