using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Web.Controllers;
using GestionVentas.Web.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Filters
{
    public class VerificarSessionAttribute:ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            int? objUsuario = (int?)SessionHelper.GetObjectFromJson<int>(context.HttpContext.Session, "usuario");
            if (objUsuario == 0) {
                if (context.ActionDescriptor.RouteValues["action"]!= "VerificarCredenciales")
                    if (!(context.Controller is AutenticacionController))
                        context.HttpContext.Response.Redirect("/autenticacion/IniciarSesion");
            }
            else
            {
                if ((context.Controller is AutenticacionController))
                    context.HttpContext.Response.Redirect("/home/index");
            }
        }
    }
}
