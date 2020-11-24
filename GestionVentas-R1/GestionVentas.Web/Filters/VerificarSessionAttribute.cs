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
            
            dynamic objUsuario = SessionHelper.GetObjectFromJson<dynamic>(context.HttpContext.Session, "usuario");
            if (objUsuario == null) {
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
