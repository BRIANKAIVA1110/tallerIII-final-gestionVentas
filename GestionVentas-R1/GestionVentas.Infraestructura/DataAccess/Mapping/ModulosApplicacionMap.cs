using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class ModulosApplicacionMap : IEntityTypeConfiguration<ModulosApplicacion>
    {
        public void Configure(EntityTypeBuilder<ModulosApplicacion> builder)
        {
            builder.ToTable("ModulosApplicacion");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Descripcion).IsRequired();
        }
    }
}
