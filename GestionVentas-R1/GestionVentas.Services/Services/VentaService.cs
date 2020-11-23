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
        
        /// <summary>
        /// genera la venta de los articulos
        /// </summary>
        /// <param name="itemsVenta"></param>
        /// <returns>VentaId</returns>
        public int GenerarVenta(List<CarroItemDTO> itemsVenta)
        {
            int result = this._ventaRepository.ProcesarVenta(itemsVenta);
            return result;
        }

        public byte[] GenerarComprobante(int p_ventaId) {

            string separador = "";
            for (int i = 0; i < 120; i++)
                separador += "*";

            StringBuilder sb = new StringBuilder();

            //seccion Empresa
            sb.AppendFormat("{0}\n\n", separador);

            sb.AppendFormat("{0}\n\n", "**COMPROBANTE NO VALIDO COMO FACTURA**");
            sb.AppendFormat("{0,-60}{1,60}\n", "Razon social: PEPE SA.", "N°: 0001-00000000");
            sb.AppendFormat("{0,-60}{1,60}\n", "Direccion: AV. FALSE 123", "FECHA: 00/00/0000");
            sb.AppendFormat("{0,-60}{1,60}\n", "Localidad: LOMAS DE ZAMORA", "CUIT: 27-00000000-5");
            sb.AppendFormat("{0,-60}\n", "Telefono: 541123456789", "");
            sb.AppendFormat("{0}\n", separador);
            //seccion cliente

            sb.AppendFormat("Nombre Cliente: {0}\n", "Villarroel Torrico brian");
            sb.AppendFormat("Domicilio: {0}\n", "AV. falsa 123");
            sb.AppendFormat("Ubicacion: {0}\n", "Lomas de zamora");
            sb.AppendFormat("Telefono: {0}\n", "12345678900");
            sb.AppendFormat("{0}\n", separador);

            //seccion detalles
            sb.AppendFormat("|{0,-9} |{1,-18} |{2,-53} |{3,-15} |{4,-15}|\n", "Cantidad", "Codigo Barras", "Descripcion", "Precio Unitario", "Importe");
            sb.AppendFormat("{0}\n", separador);
            sb.AppendFormat("|{0,-9} |{1,-18} |{2,-53} |{3,-15} |{4,-15}|\n", "2", "1234567890000000", "pepepepepepepepwwwwwwwww pepepepe pepe", "122,00", "999999999,00");
            sb.AppendFormat("{0}\n", separador);
            sb.AppendFormat("{0,110}\n", "Importe Total: $ 999999999,00");
            sb.AppendFormat("{0}\n", separador);

            byte[] ArchivoComprobante = Encoding.UTF8.GetBytes(sb.ToString());
            return ArchivoComprobante;

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
