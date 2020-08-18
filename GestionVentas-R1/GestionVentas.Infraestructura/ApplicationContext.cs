using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GestionVentas.Infraestructura
{
    public class ApplicationContext :DbContext
    {

        public ApplicationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Assembly.Load("GestionVenta.Infraestructura.DataAccess.Mapping")

        }
    }
}
