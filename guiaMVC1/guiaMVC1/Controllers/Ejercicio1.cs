using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using guiaMVC1.Models;
namespace guiaMVC1.Controllers
{
    public class Ejercicio1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult miPerfil()
        {
            return View();
        }
        public string cadenaReves(string cadena)
        {
            string arrayAlReves= "";
            cadena.ToUpper().Reverse().ToList().ForEach(c=> arrayAlReves +=c);
            return arrayAlReves;
        }
        public string compararCadena(string cadena1, string cadena2) {
            if (Equals(cadena1,cadena2)== true)
            {
                return "Las cadenas son iguales";
            }
            else {
                return "Las cadenas son diferentes";
            }
        }

  
    }
}
