using GestionVentas.Infraestructura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GestionVentas.Test.Infraestructura
{
    public class DbConnectionTest
    {
        private const string connectionString = "Server=127.0.0.1;Database=erp;Uid=root;Pwd=;";
        private const string connectionStringFalse = "Server=127.0.0.1;Database=erppepe;Uid=root;Pwd=;";
        [Fact]
        public void check_dbConnection_okk() {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            options.UseMySQL(connectionString);

            ApplicationContext context = new ApplicationContext(options.Options);

            bool canConnect = context.Database.CanConnect();
            context.Database.CloseConnection();
            Assert.True(canConnect);



        }


        [Fact]
        public void check_dbConnection_noOkk()
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            options.UseMySQL(connectionStringFalse);

            ApplicationContext context = new ApplicationContext(options.Options);

            bool canConnect = context.Database.CanConnect();
            context.Database.CloseConnection();
            Assert.False(canConnect);



        }

        //[Theory]
        //[InlineData(1)]
    }
}
