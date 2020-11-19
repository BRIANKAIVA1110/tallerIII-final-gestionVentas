using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Services.Services
{
    public class VentaService:IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        public VentaService(IVentaRepository ventaRepository) {
            this._ventaRepository = ventaRepository;
        }
        
        public int GenerarVenta(List<CarroItemDTO> itemsVenta)
        {
            this._ventaRepository.ProcesarVenta(itemsVenta);

            return 1;
        }

        public int EliminarVenta(int p_id)
        {
            throw new NotImplementedException();
        }

        public VentaDTO getVenta(int p_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VentaDTO> getVentas()
        {
            throw new NotImplementedException();
        }

        public int ModificarVenta(VentaDTO p_colorDTO)
        {
            throw new NotImplementedException();
        }
    }
}
