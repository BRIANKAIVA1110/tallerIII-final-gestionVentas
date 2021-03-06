﻿using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class ModulosXPerfileMap : IEntityTypeConfiguration<ModulosXPerfil>
    {
        public void Configure(EntityTypeBuilder<ModulosXPerfil> builder)
        {
            builder.ToTable("modulosxperfiles");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ModuloId).IsRequired();
            builder.Property(x => x.PerfilId).IsRequired();
            
        }
    }
}
