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
        private readonly IMapper _mapper;
        public VentasController( IMapper mapper, IArticuloService articuloService) {

            this._mapper = mapper;
            this._articuloService = articuloService;
        
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
        public IActionResult AgregarArticulo([FromQuery]int IdArticulo)
        {

            //que pasaa si no existe, gestionar esto con un condicional.
            ArticuloDTO articuloDTO = this._articuloService.getArticulo(IdArticulo);


            if (SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart") == null)
            {
                CarroCompras cart = new CarroCompras();
                cart.Articulos.Add(new ArticuloDTO
                { 
                   Id = articuloDTO.Id,
                   Descripcion = articuloDTO.Descripcion,
                   CategoriaDescripcion = articuloDTO.CategoriaDescripcion,
                   ModeloDescripcion = articuloDTO.ModeloDescripcion,
                   ColorDescripcion = articuloDTO.ColorDescripcion
                   
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
                int index = isExist(IdArticulo);
                if (index != -1)
                {
                    //cart.CarroArticulos[index].ca++;
                }
                else
                {
                    cart.Articulos.Add(new ArticuloDTO { 
                        Id = articuloDTO.Id, 
                        Descripcion = articuloDTO.Descripcion,
                        ModeloDescripcion = articuloDTO.ModeloDescripcion
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("EliminarArticulo/{IdArticulo}")]
        public IActionResult Remove(int IdArticulo)
        {
            CarroCompras cart = SessionHelper.GetObjectFromJson<CarroCompras>(HttpContext.Session, "cart");
            int index = isExist(IdArticulo);
            cart.Articulos.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
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
