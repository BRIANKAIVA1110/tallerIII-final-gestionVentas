using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Filters;
using GestionVentas.Web.Models.ViewModels.Articulos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    [VerificarAutorizacionModulo(ModulosAplicacionEnum.articulos)]
    public class ArticulosController:Controller
    {
        private readonly IArticuloService _articuloService;
        private readonly IColorService _colorService;
        private readonly IModeloService _modeloService;
        private readonly IMarcaService _marcaService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;
        public ArticulosController(IArticuloService articuloService, IColorService colorService, IModeloService modeloService, IMarcaService marcaService, ICategoriaService categoriaService, IMapper mapper) 
        {
            this._articuloService = articuloService;
            this._colorService = colorService;
            this._modeloService = modeloService;
            this._marcaService = marcaService;
            this._categoriaService = categoriaService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<ArticuloViewModel> listArticuloViewModels = this._articuloService.getArticulos()
                .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x)).ToList();

                return View(listArticuloViewModels);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Agregar(ArticuloViewModel p_articuloVM)
        {
            try
            {
                //this._articuloService.AgregarArticulo(p_articuloDTO);
                if (!ModelState.IsValid)
                    return View("error");
                else
                {
                    var articuloDTO = this._mapper.Map<ArticuloViewModel, ArticuloDTO>(p_articuloVM);
                    int result = this._articuloService.AgregarArticulo(articuloDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<ArticuloViewModel> listArticuloViewModels = this._articuloService.getArticulos()
                    .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x)).ToList();

                    return View("index",listArticuloViewModels);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_articuloVM);
            }



        }
        public IActionResult Modificar(ArticuloViewModel p_articuloVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("error");
                else
                {
                    ArticuloDTO articuloDTO = this._mapper.Map<ArticuloViewModel, ArticuloDTO>(p_articuloVM);
                    int result = this._articuloService.ModificarArticulo(articuloDTO);

                    ViewBag.result = "Accion realizada con exito.";

                    List<ArticuloViewModel> listArticuloViewModels = this._articuloService.getArticulos()
                    .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x)).ToList();

                    return View("index", listArticuloViewModels);
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;

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

                List<SelectListItem> marcas = this._marcaService.getMarcas()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Codigo} - {x.Descripcion}"
                    }).ToList();

                List<SelectListItem> categorias = this._categoriaService.getCategorias()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Codigo} - {x.Descripcion}"
                    }).ToList();
                p_articuloVM.Modelos= modelos;
                p_articuloVM.Colores = colores;
                p_articuloVM.Marcas= marcas;
                p_articuloVM.Categorias= categorias;
                return View("form", p_articuloVM);
            }

        }
        public IActionResult Detalle(int Id)
        {
            try
            {
                ArticuloDTO articuloDTO = this._articuloService.getArticulo((int)Id);
                if (articuloDTO != null)
                {
                    ArticuloViewModel articuloViewModel = this._mapper.Map<ArticuloDTO, ArticuloViewModel>(articuloDTO);
                    return View(articuloViewModel);
                }
                else
                {
                    ViewBag.error = "Ocurrio un erro al intentar obtener el registro solicitado.";
                    return View("index"); //deberia mostrar un msg de notificacion
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }

        }

        public IActionResult Buscar([FromQuery] string p_query)
        {

            try
            {
                //ver diferencias: contains vs like method
                if (p_query != null)
                {
                    List<ArticuloViewModel> listArticuloViewModel = this._articuloService.getArticulos()
                    .Where(x => x.CodigoBarras.Contains(p_query) ||
                        x.Descripcion.Contains(p_query) ||
                        x.ModeloDescripcion.Contains(p_query) ||
                        x.ColorDescripcion.Contains(p_query) ||
                        x.CategoriaDescripcion.Contains(p_query) ||
                        x.MarcaDescripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x))
                    .ToList();

                    if (!listArticuloViewModel.Any())
                        ViewBag.info = "No se encontraron registros";

                    return View("index", listArticuloViewModel);
                }
                else
                {
                    List<ArticuloViewModel> listArticuloViewModel = this._articuloService.getArticulos()
                    .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x))
                    .ToList();
                    return View("index", listArticuloViewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }


        }

        public IActionResult Eliminar(int Id)
        {
            try
            {
                var result = this._articuloService.EliminarArticulo(Id);

                ViewBag.result = "Accion realizada con exito.";

                List<ArticuloViewModel> listArticuloViewModel = this._articuloService.getArticulos()
                    .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x))
                    .ToList();
                return View("index", listArticuloViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<ArticuloViewModel> listArticuloViewModel = this._articuloService.getArticulos()
                    .Select(x => this._mapper.Map<ArticuloDTO, ArticuloViewModel>(x))
                    .ToList();
                return View("index", listArticuloViewModel);

            }
            
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
            try
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

                    List<SelectListItem> marcas = this._marcaService.getMarcas()
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = $"{x.Codigo} - {x.Descripcion}"
                        }).ToList();

                    List<SelectListItem> categorias = this._categoriaService.getCategorias()
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = $"{x.Codigo} - {x.Descripcion}"
                        }).ToList();

                    if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                    {

                        articuloViewModel.Colores = colores;
                        articuloViewModel.Modelos = modelos;
                        articuloViewModel.Marcas = marcas;
                        articuloViewModel.Categorias = categorias;

                        return View(articuloViewModel);
                    }
                    if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                    {
                        ArticuloDTO articuloDTO = this._articuloService.getArticulo((int)Id);
                        articuloViewModel = this._mapper.Map<ArticuloDTO, ArticuloViewModel>(articuloDTO);
                        articuloViewModel.Colores = colores;
                        articuloViewModel.Modelos = modelos;
                        articuloViewModel.Marcas = marcas;
                        articuloViewModel.Categorias = categorias;
                        return View(articuloViewModel);
                    }

                }

                throw new Exception("Ocurrio un error inesperado.");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }

        public IActionResult ExportarRegistros()
        {
            try
            {
                byte[] file = this._articuloService.GenerarExportacionRegistros();

                FileContentResult File = new FileContentResult(file, "application/CSV")
                {
                    FileDownloadName = $"articulos_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
                };
                return File;
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View("index");
            }



        }
    }
}
