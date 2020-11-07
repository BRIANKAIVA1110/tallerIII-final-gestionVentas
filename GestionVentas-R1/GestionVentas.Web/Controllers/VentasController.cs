using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Controllers
{
    public class VentasController: Controller
    {
        public VentasController() { }


        public IActionResult Index() {
            return View();
        }
    }
}
