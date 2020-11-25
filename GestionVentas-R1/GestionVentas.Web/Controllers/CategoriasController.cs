using AutoMapper;
using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Services.Services;
using GestionVentas.Web.Enum;
using GestionVentas.Web.Models.ViewModels.Articulos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GestionVentas.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaService categoriaService, IMapper mapper)
        {
            this._categoriaService = categoriaService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<CategoriaViewModel> categoriaViewModel = this._categoriaService.getCategorias()
                .Select(x => this._mapper.Map<CategoriaViewModel>(x)).ToList();

                return View(categoriaViewModel);
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                return View();
            }
            

        }

        [HttpPost]
        public IActionResult Agregar(CategoriaViewModel p_categoriaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Error al validar datos.");
                else
                {
                    CategoriaDTO categoriaDTO = this._mapper.Map<CategoriaDTO>(p_categoriaVM);
                    int result = this._categoriaService.AgregarCategoria(categoriaDTO);

                    ViewBag.result = "Accion realizada con exito.";
                    List<CategoriaViewModel> categoriaViewModel = this._categoriaService.getCategorias()
                    .Select(x => this._mapper.Map<CategoriaViewModel>(x)).ToList();

                    return View("index",categoriaViewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.AGREGAR;
                return View("form", p_categoriaVM);
            }

        }


        public IActionResult Modificar(CategoriaViewModel p_categoriaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("error");
                else
                {
                    CategoriaDTO categoriaDTO = this._mapper.Map<CategoriaDTO>(p_categoriaVM);
                    int result = this._categoriaService.ModificarCategoria(categoriaDTO);
                    ViewBag.result = "Accion realizada con exito.";

                    List<CategoriaViewModel> categoriaViewModel = this._categoriaService.getCategorias()
                    .Select(x => this._mapper.Map<CategoriaViewModel>(x)).ToList();

                    return View("index", categoriaViewModel);

                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.InnerException.Message;
                ViewData["accionCRUD"] = AccionesCRUD.MODIFICAR;
                return View("form", p_categoriaVM);
            }

        }

        public IActionResult Detalle(int Id)
        {
            try
            {
                CategoriaDTO categoriaDTO = this._categoriaService.getCategoria((int)Id);
                if (categoriaDTO != null)
                {
                    CategoriaViewModel categoriaViewModel = this._mapper.Map<CategoriaDTO, CategoriaViewModel>(categoriaDTO);
                    return View(categoriaViewModel);
                }
                else
                {
                    ViewBag.error = "Ocurrio un erro al intentar obtener el registro solicitado.";
                    return View("index"); 
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
                    List<CategoriaViewModel> listCategoriaViewModel = this._categoriaService.getCategorias()
                    .Where(x => x.Codigo.Contains(p_query) ||
                        x.Descripcion.Contains(p_query))
                    .Select(x => this._mapper.Map<CategoriaViewModel>(x))
                    .ToList();

                    if (!listCategoriaViewModel.Any())
                        ViewBag.info = "No se encontraron registros";
                    return View("index", listCategoriaViewModel);
                }
                else
                {
                    List<CategoriaViewModel> listCategoriaViewModel = this._categoriaService.getCategorias()
                    .Select(x => this._mapper.Map<CategoriaViewModel>(x))
                    .ToList();
                    return View("index", listCategoriaViewModel);
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
                var result = this._categoriaService.EliminarCategoria(Id);

                ViewBag.result = "Accion realizada con exito.";

                List<CategoriaViewModel> listCategoriaViewModel = this._categoriaService.getCategorias()
                    .Select(x => this._mapper.Map<CategoriaViewModel>(x))
                    .ToList();

                return View("index", listCategoriaViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.error = $"{ex.Message} El registro no debe estar referenciado con otro para su eliminacion.";
                List<CategoriaViewModel> listCategoriaViewModel = this._categoriaService.getCategorias()
                    .Select(x => this._mapper.Map<CategoriaViewModel>(x))
                    .ToList();

                return View("index", listCategoriaViewModel);
            }
            
        }

        /// <summary>
        /// action renderiza formulario para las acciones agregar || modificar, "reutilizacion"
        /// </summary>
        /// <param name="accionCRUD"> AGREGAR || MODIFICAR </param>
        /// <param name="Id"> null || Id </param>
        /// <returns></returns>
        //[Route("Categoria/Form")]
        [Route("Categoria/Form/{Id?}")]
        public IActionResult Form([FromQuery] AccionesCRUD accionCRUD, int? Id)
        {
            try
            {
                if (accionCRUD.Equals(AccionesCRUD.AGREGAR) || accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                {

                    ViewData["accionCRUD"] = accionCRUD;
                    if (accionCRUD.Equals(AccionesCRUD.AGREGAR))
                        return View();

                    if (accionCRUD.Equals(AccionesCRUD.MODIFICAR))
                    {
                        CategoriaDTO categoriaDTO = this._categoriaService.getCategoria((int)Id);
                        CategoriaViewModel categoriaViewModel = this._mapper.Map<CategoriaViewModel>(categoriaDTO);
                        return View(categoriaViewModel);
                    }

                }

                throw new Exception("Ocurrio un error inesperado.");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("index");
            }

        }

        public IActionResult ExportarRegistros()
        {
            try
            {
                byte[] file = this._categoriaService.GenerarExportacionRegistros();

                FileContentResult File = new FileContentResult(file, "application/CSV")
                {
                    FileDownloadName = $"categorias_export_{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.csv",
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
