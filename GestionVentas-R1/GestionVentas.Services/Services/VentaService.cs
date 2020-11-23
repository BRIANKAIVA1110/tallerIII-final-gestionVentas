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
        private readonly IEmpresaRepository _empresaRepository;
        public VentaService(IVentaRepository ventaRepository, IEmpresaRepository empresaRepository) {
            this._ventaRepository = ventaRepository;
            this._empresaRepository = empresaRepository;
        }
        
        /// <summary>
        /// genera la venta de los articulos
        /// </summary>
        /// <param name="itemsVenta"></param>
        /// <returns>VentaId</returns>
        public int GenerarVenta(List<CarroItemDTO> itemsVenta, ClienteDTO p_cliente)
        {
            int result = this._ventaRepository.ProcesarVenta(itemsVenta, p_cliente);
            return result;
        }

        public byte[] GenerarComprobante(int p_ventaId) {

            //TODO: falta agregar el get de la venta.
            List<DetalleVenta> detallesVenta = this._ventaRepository.ObtenerDetalleVenta(p_ventaId).ToList();
            Venta objVenta = detallesVenta[0].Venta;

            //treamos informacio de la empresa.
            int empresaId = 1;//hay una unica empresa... meter a config??
            Empresa objEmpresa = this._empresaRepository.GetById(empresaId);

            string separador = "";
            for (int i = 0; i < 120; i++)
                separador += "*";

            StringBuilder sb = new StringBuilder();

            //seccion Empresa
            sb.AppendFormat("{0}\n\n", separador);

            sb.AppendFormat("{0}\n\n", "**COMPROBANTE NO VALIDO COMO FACTURA**");
            sb.AppendFormat("{0,-60}{1,60}\n", $"Razon social: {objEmpresa.RazonSocial}", $"N°: 0001-{objVenta.Id.ToString().PadLeft(8,'0')}");
            sb.AppendFormat("{0,-60}{1,60}\n", $"Direccion: {objEmpresa.Domicilio.Split(",")[0]}", $"FECHA: {objVenta.FechaVenta.ToString("dd-MM-yyyy")}");
            sb.AppendFormat("{0,-60}{1,60}\n", $"Localidad: {objEmpresa.Domicilio.Split(", ")[1]},{objEmpresa.Domicilio.Split(", ")[2]}", "CUIT: 27-00000000-5");
            sb.AppendFormat("{0,-60}\n", $"Telefono: {objEmpresa.Telefono}", "");
            sb.AppendFormat("{0}\n", separador);
            //seccion cliente

            sb.AppendFormat("{0}\n", $"Nombre Cliente: {objVenta.ClienteInformacion.Split(";")[0]}");
            sb.AppendFormat("{0}\n", $"Domicilio: {objVenta.ClienteInformacion.Split(";")[1]}");
            sb.AppendFormat("{0}\n", $"Localidad: {objVenta.ClienteInformacion.Split(";")[2]}");
            sb.AppendFormat("{0}\n", $"Codigo postal: {objVenta.ClienteInformacion.Split(";")[3]}");
            sb.AppendFormat("{0}\n", $"Telefono: {objVenta.ClienteInformacion.Split(";")[4]}");
            sb.AppendFormat("{0}\n", separador);

            //seccion detalles
            sb.AppendFormat("|{0,-9} |{1,-18} |{2,-53} |{3,-15} |{4,-15}|\n", "Cantidad", "Codigo Barras", "Descripcion", "Precio Unitario", "Importe");
            sb.AppendFormat("{0}\n", separador);
            foreach (var item in detallesVenta)
            {
                sb.AppendFormat("|{0,-9} |{1,-18} |{2,-53} |{3,-15} |{4,-15}|\n", $"{item.CantidadUnidades}", $"{item.Articulo.CodigoBarras}", $"{item.Articulo.Descripcion}", $"$ {item.Articulo.Precio}", $"$ {item.Articulo.Precio* item.CantidadUnidades}");
            }
            
            sb.AppendFormat("{0}\n", separador);
            sb.AppendFormat("{0,110}\n", $"Importe Total: $ {objVenta.TotalFinal}");
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
