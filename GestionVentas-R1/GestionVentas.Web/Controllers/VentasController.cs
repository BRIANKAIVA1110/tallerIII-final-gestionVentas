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
    public class VentasController: Controller
    {

        private readonly IArticuloService _articuloService;
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;
        public VentasController( IMapper mapper, IArticuloService articuloService, IVentaService ventaService) {

            this._mapper = mapper;
            this._articuloService = articuloService;
            this._ventaService = ventaService;
        }


        public IActionResult Index() {
            var cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            VentaViewModel ventaViewModel = new VentaViewModel();
            ventaViewModel.CarroArticulos = cart;
            //ViewBag.cart = cart;
            //ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View(ventaViewModel);
        }

        [HttpGet]
        public IActionResult AgregarArticulo([FromQuery]string p_codigoBarras, [FromQuery] int cantidadUnideades)
        {

            //que pasaa si no existe, gestionar esto con un condicional.
            ArticuloDTO articuloDTO = this._articuloService.ObtenerArticuloPorCodigoBarras(p_codigoBarras);


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
            }
            return RedirectToAction("Index");
        }

        public IActionResult EliminarArticulo([FromQuery]int IdArticulo)
        {
            CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            int index = isExist(IdArticulo);
            cart.Articulos.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult venderArticulos() {

            CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            if (cart.Articulos.Count != 0)
            {
                var result = this._ventaService.GenerarVenta(cart.Articulos);
                if (result > 0)
                {
                    cart = null;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    return RedirectToAction("index");
                }
                else {
                    return RedirectToAction("index");
                }
            }
            else {
                return RedirectToAction("index");
            }
            
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
