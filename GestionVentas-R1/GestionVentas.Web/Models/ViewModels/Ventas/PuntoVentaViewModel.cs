﻿using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Models.ViewModels.Ventas
{
    public class PuntoVentaViewModel
    {
        public CarroCompras CarroArticulos { get; set; }
        public ClienteDTO InformacionCliente { get; set; }
        public int VentaId { get; set; }
    }
    public class CarroCompras
    {

        public List<CarroItemDTO> Articulos { get; set; }

        public decimal Cantidad { get; set; }


        public CarroCompras()
        {
            this.Articulos = new List<CarroItemDTO>();
        }

    }
}
