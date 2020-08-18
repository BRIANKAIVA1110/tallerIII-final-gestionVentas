using System;
using System.Collections.Generic;
using System.Text;
using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.Data.EntityFrameworkCore;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class ModeloMap : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.ToTable("Modelos");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired();
        }
    }
}
