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

            List<CategoriaViewModel> categoriaViewModel = this._categoriaService.getCategorias()
                .Select(x => this._mapper.Map<CategoriaViewModel>(x)).ToList();

            return View(categoriaViewModel);

        }

        [HttpPost]
        public IActionResult Agregar(CategoriaViewModel p_categoriaVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                CategoriaDTO categoriaDTO = this._mapper.Map<CategoriaDTO>(p_categoriaVM);
                int result = this._categoriaService.AgregarCategoria(categoriaDTO);
                if (result > 0)
                    return RedirectToAction("index");
                else
                    return RedirectToAction("FORM");//deberia mostrar un error Y PASAMOS DATOS POR VIEWBAG O DATA
            }
        }


        public IActionResult Modificar(CategoriaViewModel p_categoriaVM)
        {
            if (!ModelState.IsValid)
                return View("error");
            else
            {
                CategoriaDTO categoriaDTO = this._mapper.Map<CategoriaDTO>(p_categoriaVM);
                int result = this._categoriaService.ModificarCategoria(categoriaDTO);

                if (result > 0)
                    return RedirectToAction("index");
                else
                    return View("form");
            }
        }

        public IActionResult Detalle(int Id)
        {
            CategoriaDTO categoriaDTO = this._categoriaService.getCategoria((int)Id);
            if (categoriaDTO != null)
            {
                CategoriaViewModel categoriaViewModel = this._mapper.Map<CategoriaDTO, CategoriaViewModel>(categoriaDTO);
                return View(categoriaViewModel);
            }
            else
            {
                return View("index"); //deberia mostrar un msg de notificacion
            }
        }

        public IActionResult Buscar([FromQuery] string p_query)
        {
            //ver diferencias: contains vs like method
            if (p_query != null)
            {
                List<CategoriaViewModel> listCategoriaViewModel = this._categoriaService.getCategorias()
                .Where(x => x.Codigo.Contains(p_query) ||
                    x.Descripcion.Contains(p_query))
                .Select(x => this._mapper.Map<CategoriaViewModel>(x))
                .ToList();

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

        public IActionResult Eliminar(int Id)
        {

            var result = this._categoriaService.EliminarCategoria(Id);

            return RedirectToAction("index");
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
            return RedirectToAction("ERROR", "HOME");
        }
    }
}
