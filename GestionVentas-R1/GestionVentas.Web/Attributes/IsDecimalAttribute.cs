using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionVentas.Web.Attributes
{
    /// <summary>
    /// verifica formato precio decimal(9,2)
    /// </summary>
    public class IsDecimalAttribute:ValidationAttribute
    {
            public IsDecimalAttribute() { }

            //este metodo se ejecuta cuando se hace post... no lo aplica en el cliente.. ver como hacerlo
            public override bool IsValid(object value)
            {
                Regex regx = new Regex(@"\A([0-9]{1,9}\Z)|\A([0-9]{1,9}[,][0-9]{2})\Z");
                bool result = regx.IsMatch((string)value);

                return result;
            }
        
    }
}
