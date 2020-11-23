using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Helper;
using GestionVentas.Web.Models.ViewModels.Ventas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class VentasController : Controller
    {

        private readonly IArticuloService _articuloService;
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;
        public VentasController(IMapper mapper, IArticuloService articuloService, IVentaService ventaService) {

            this._mapper = mapper;
            this._articuloService = articuloService;
            this._ventaService = ventaService;
        }


        public IActionResult Index(){

            CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            var clienteInfo = SessionHelper.GetObjectFromJson<ClienteDTO>(HttpContext.Session, "clienteInformacion");
            int ventaId = (int)SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "ventaId");

            VentaViewModel ventaViewModel = new VentaViewModel();
            ventaViewModel.CarroArticulos = cart;
            ventaViewModel.VentaId = ventaId;
            ventaViewModel.InformacionCliente = clienteInfo;

            /*
             * si hubo una venta "reinicia" ventaId a "0", el cual indica que habra una nueva venta y no
             * imprimira un comprobante de venta
             * */
            if (ventaId != 0) {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "ventaId", 0);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "clienteInformacion", null);
            }
                

            return View(ventaViewModel);
        }

        [HttpGet]
        public IActionResult AgregarArticulo([FromQuery] string p_codigoBarras, [FromQuery] int cantidadUnideades, VentaViewModel ventaViewModel)
        {
            try
            {
                bool hayStoy = false;
                //debe traer el registro de un articulo el cual tiene asignado stock, sino retorna null
                ArticuloDTO articuloDTO = this._articuloService.ObtenerArticuloPorCodigoBarras(p_codigoBarras);
                if (articuloDTO != null)
                    hayStoy = (articuloDTO.CantidadStock - cantidadUnideades) >= 0;

                if (articuloDTO != null && hayStoy)
                {
                    if (SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart") == null)
                    {
                        CarroCompras cart = new CarroCompras();
                        cart.Articulos.Add(new CarroItemDTO
                        {
                            Id = articuloDTO.Id,
                            Descripcion = articuloDTO.Descripcion,
                            CategoriaDescripcion = articuloDTO.CategoriaDescripcion,
                            ModeloDescripcion = articuloDTO.ModeloDescripcion,
                            ColorDescripcion = articuloDTO.ColorDescripcion,
                            Precio = articuloDTO.Precio,
                            CantidadUnidades = cantidadUnideades,
                            Total = cantidadUnideades * articuloDTO.Precio,

                        });
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        AlmacenarInformacionClienteSession(ventaViewModel);
                    }
                    else
                    {
                        CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
                        int index = isExist(articuloDTO.Id);
                        if (index != -1)
                        {
                            //cart.CarroArticulos[index].ca++;
                        }
                        else
                        {
                            cart.Articulos.Add(new CarroItemDTO
                            {
                                Id = articuloDTO.Id,
                                Descripcion = articuloDTO.Descripcion,
                                ModeloDescripcion = articuloDTO.ModeloDescripcion,
                                ColorDescripcion = articuloDTO.ColorDescripcion,
                                Precio = articuloDTO.Precio,
                                CantidadUnidades = cantidadUnideades,
                                Total = cantidadUnideades * articuloDTO.Precio,
                            });
                        }
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        AlmacenarInformacionClienteSession(ventaViewModel);


                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    if (!hayStoy)
                    {
                        ViewBag.info = $"El articulo ingresado no tiene stock disponible para la cantidad ingresada. su stock disponible es de: {articuloDTO?.CantidadStock}.";
                    }
                    if (!(articuloDTO != null))
                    {
                        ViewBag.info = "El articulo no tiene stock asignado, solo se puede agregar articulos con stock asignado.";
                    }

                    var cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
                    var clienteInfo = SessionHelper.GetObjectFromJson<ClienteDTO>(HttpContext.Session, "clienteInformacion");

                    

                    return RedirectToAction("index");
                }


            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return RedirectToAction("Index");
            }


        }

        private void AlmacenarInformacionClienteSession(VentaViewModel ventaViewModel) {
            ClienteDTO objClienteDTO = new ClienteDTO();
            ClienteDTO clienteSession = SessionHelper.GetObjectFromJson<ClienteDTO>(HttpContext.Session, "clienteInformacion");

            objClienteDTO.NombreCompleto = ventaViewModel.InformacionCliente?.NombreCompleto ?? clienteSession?.NombreCompleto;
            objClienteDTO.Domicilio = ventaViewModel.InformacionCliente?.Domicilio ?? clienteSession?.Domicilio;
            objClienteDTO.Localidad = ventaViewModel.InformacionCliente?.Localidad ?? clienteSession?.Localidad;
            objClienteDTO.CodiPostal = ventaViewModel.InformacionCliente?.CodiPostal ?? clienteSession?.CodiPostal;
            objClienteDTO.Telefono = ventaViewModel.InformacionCliente?.Telefono ?? clienteSession?.Telefono;

            SessionHelper.SetObjectAsJson(HttpContext.Session, "clienteInformacion", objClienteDTO);
        }
        public IActionResult EliminarArticulo([FromQuery] int IdArticulo)
        {
            CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            int index = isExist(IdArticulo);
            cart.Articulos.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public  IActionResult VenderArticulos(VentaViewModel p_ventaViewModel) {
            try
            {
                CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
                if (cart == null)
                {
                    throw new Exception("Debe agregar articulos al carro de compras para generar una venta");
                }
                if (cart.Articulos?.Count != 0)
                {
                    int ventaId = this._ventaService.GenerarVenta(cart.Articulos, p_ventaViewModel.InformacionCliente);
                    if (ventaId != 0)
                    {
                        cart = null;
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);//TODO: verificar si esto esta de mas
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "clienteInformacion", null);//TODO: verificar si esto esta de mas
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "ventaId", ventaId.ToString());
                        ViewBag.result = "Se realizo la venta con exito.";
                    }
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.info = "Debe agregar articulos al carro de compras.";
                    return RedirectToAction("index");
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;

                AlmacenarInformacionClienteSession(p_ventaViewModel);
                 
                return View("index", p_ventaViewModel);
            }
            
        }

        
        public IActionResult DescargarComprobanteVenta(int ventaId) {

            byte[] result = this._ventaService.GenerarComprobante(ventaId);
            FileContentResult file = new FileContentResult(result, "text/plain");
            file.FileDownloadName = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-comprobanteDeVenta.txt";
            return file;
        }
        private int isExist(int IdArticulo)
        {
            CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Articulos.Count; i++)
            {
                if (cart.Articulos[i].Id.Equals(IdArticulo))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
