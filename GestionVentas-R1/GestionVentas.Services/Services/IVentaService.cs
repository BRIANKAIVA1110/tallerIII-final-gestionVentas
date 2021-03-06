﻿using GestionVentas.DataTransferObjects.EntityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Services
{
    public interface IVentaService
    {
        int GenerarVenta(List<CarroItemDTO> itemsVenta, ClienteDTO p_cliente);
        byte[] GenerarComprobante(int p_ventaId);
        int ModificarVenta(VentaDTO p_colorDTO);
        int EliminarVenta(int p_id);
        VentaDTO getVenta(int p_id);
        IEnumerable<VentaDTO> getVentas();
    }
}
