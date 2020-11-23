using System;
using System.Collections.Generic;
using System.Text;
using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionVentas.Infraestructura.DataAccess.Mapping
{
    public class EmpresaMAP : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa");
            builder.Property(x=> x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RazonSocial).IsRequired();
            builder.Property(x => x.Telefono).IsRequired();
            builder.Property(x => x.Cuit).IsRequired();
            builder.Property(x => x.Domicilio).IsRequired();
           
        }
    }
}
