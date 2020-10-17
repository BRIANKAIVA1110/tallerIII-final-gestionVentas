﻿using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Models.ViewModels.Articulos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class ArticulosController:Controller
    {
        private readonly IArticuloService _articuloService;
        private readonly IColorService _colorService;
        private readonly IModeloService _modeloService;
        private readonly IMapper _mapper;
        public ArticulosController(IArticuloService articuloService, IColorService colorService, IModeloService modeloService, IMapper mapper) 
        {
            this._articuloService = articuloService;
            this._colorService = colorService;
            this._modeloService = modeloService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {

            List<ArticuloViewModel> listArticuloViewModels = this._articuloService.getArticulos()
                .Select(x=> this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x)).ToList();

            return View(listArticuloViewModels);
        }

        [HttpPost]
        public IActionResult Agregar(ArticuloViewModel p_articuloVM)
        {
            //this._articuloService.AgregarArticulo(p_articuloDTO);
            if (!ModelState.IsValid)
                return View("error");
            else {
                var articuloDTO = this._mapper.Map<ArticuloViewModel,ArticuloDTO>(p_articuloVM);
                int result = this._articuloService.AgregarArticulo(articuloDTO);
                if(result>0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }

            
        }
        public IActionResult Modificar(ArticuloViewModel p_articuloVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                ArticuloDTO articuloDTO = this._mapper.Map<ArticuloViewModel, ArticuloDTO>(p_articuloVM);
                int result = this._articuloService.ModificarArticulo(articuloDTO);
                if (result > 0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }
        }
        public IActionResult Detalle(int Id)
        {

            ArticuloDTO articuloDTO  = this._articuloService.getArticulo((int)Id);
            if (articuloDTO != null)
            {
                ArticuloViewModel articuloViewModel = this._mapper.Map<ArticuloDTO, ArticuloViewModel>(articuloDTO);
                return View(articuloViewModel);
            }
            else{
                return View("index");//deberia mostrar un msg de exepcion.. "no se encontro registro, etc"
            }
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            if (p_query != null)
            {
                List<ArticuloViewModel> listArticuloViewModel = this._articuloService.getArticulos()
                .Where(x => x.Codigo.Contains(p_query) || 
                    x.Descripcion.Contains(p_query) ||
                    x.ModeloDescripcion.Contains(p_query) || 
                    x.ColorDescripcion.Contains(p_query))
                .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x))
                .ToList();

                return View("index", listArticuloViewModel);
            }
            else {
                List<ArticuloViewModel> listArticuloViewModel = this._articuloService.getArticulos()
                .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x))
                .ToList();
                return View("index", listArticuloViewModel);
            }
            
        }

        public IActionResult Eliminar(int Id)
        {

            var result = this._articuloService.EliminarArticulo(Id);

            return RedirectToAction("index");
        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Articulos/Form")]
        [Route("Articulos/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id)
        {

            if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
            {
                ViewData["accionCRUD"] = accionCRUD;

                ArticuloViewModel articuloViewModel = new ArticuloViewModel();

                List<SelectListItem> modelos = this._modeloService.getModelos()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Codigo} - {x.Descripcion}"
                }).ToList();

                List<SelectListItem> colores = this._colorService.getColores()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Codigo} - {x.Descripcion}"
                    }).ToList();

                if (accionCRUD.Equals(AccionesCRUD.AGREGAR)) {

                    articuloViewModel.Colores = colores;
                    articuloViewModel.Modelos = modelos;

                    return View(articuloViewModel);
                }
                if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {
                    ArticuloDTO articuloDTO = this._articuloService.getArticulo((int)Id);
                    articuloViewModel = this._mapper.Map<ArticuloDTO, ArticuloViewModel>(articuloDTO);
                    articuloViewModel.Colores = colores;
                    articuloViewModel.Modelos = modelos;
                    return View(articuloViewModel);
                }

            }
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
