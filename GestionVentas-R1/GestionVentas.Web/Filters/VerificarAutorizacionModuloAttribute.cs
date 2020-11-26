using GestionVentas.Web.Enum;
using GestionVentas.Web.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionVentas.Web.Filters
{
    public class VerificarAutorizacionModuloAttribute:ActionFilterAttribute
    {
        private readonly ModulosAplicacionEnum _Modulo;
        public VerificarAutorizacionModuloAttribute(ModulosAplicacionEnum modulo)
        {
            this._Modulo = modulo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            dynamic objUsuario = SessionHelper.GetObjectFromJson<dynamic>(context.HttpContext.Session, "usuario");
            string ModulosAutorizado = objUsuario.ModulosAutorizado;
            if (ModulosAutorizado.Contains(_Modulo.ToString()))
                base.OnActionExecuting(context);
            else {
                context.HttpContext.Response.Redirect("/home/index");
            }
        }
    }
}
