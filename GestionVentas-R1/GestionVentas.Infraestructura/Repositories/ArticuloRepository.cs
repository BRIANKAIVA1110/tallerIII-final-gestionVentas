﻿using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class ArticuloRepository:RepositoryBase<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(ApplicationContext applicationContext, IDbConnection connection) : base(applicationContext, connection) { }


        public override Articulo GetById(int p_id)
        {
            try
            {
                Articulo result = this._entity.Include(x => x.Modelo)
               .Include(x => x.Color)
               .Include(x => x.Marca)
               .Include(x => x.Categoria)
               .FirstOrDefault(x => x.Id == p_id);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public override IEnumerable<Articulo> Get()
        {
            try
            {
                IEnumerable<Articulo> listResult = this._entity.Include(x => x.Modelo)
                .Include(x => x.Color)
                .Include(x => x.Marca)
                .Include(x => x.Categoria)
                .ToList();

                return listResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
