using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IVentaRepository: IRepositoryBase<Venta>
    {
        int ProcesarVenta(List<CarroItemDTO> p_carroItems);
    }
}
