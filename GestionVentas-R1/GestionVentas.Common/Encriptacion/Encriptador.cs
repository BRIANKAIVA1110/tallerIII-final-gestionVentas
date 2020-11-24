using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionVentas.Common.Encriptacion
{
    public static class Encriptador
    {
        public static string Encriptar(string valorAEncriptar) {
            byte[] encriptacion = Encoding.UTF8.GetBytes(valorAEncriptar);
            string resultEncriptacion = Convert.ToBase64String(encriptacion);
            return resultEncriptacion;
        }

        public static string Desencriptar(string valorADesencriptar) {
            byte[] descrip = Convert.FromBase64String(valorADesencriptar);
            string resultdescrip = Encoding.UTF8.GetString(descrip, 0, descrip.ToArray().Length);
            return resultdescrip;
        }
    }
}
