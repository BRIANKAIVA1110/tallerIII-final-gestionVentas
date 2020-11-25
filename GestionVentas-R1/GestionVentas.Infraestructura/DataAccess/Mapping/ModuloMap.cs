using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class ModuloMap : IEntityTypeConfiguration<Modulo>
    {
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            builder.ToTable("Modulos");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Descripcion).IsRequired();
        }
    }
}
