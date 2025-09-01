using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace guiaMVC1.Controllers
{

    public class Ejercicio2 : Controller
    {
        private string[] Nombres = { "Diego", "Francisco", "Ismael", "Josias", "Kevin" };

        public IActionResult Index()
        {
            return View();
        }

        public string posicionArray(int numero) {
            int i;
            string posicionNombre="";
            if (numero != null || numero >= 0)
            {
                for (i = 0; i < Nombres.Length; i++)
                {
                    if (numero == i)
                    {
                        posicionNombre = Nombres[i];
                    }
                }
            }
            else {
                return "El numero esta fuera delrango del Array";
            }
            return "El nombre ubicado en la posicion " + numero + " es:" + posicionNombre;
        }

        public string busqueCadena(string nombre) {
            if (nombre != null & nombre != "")
            {
                foreach (string buscarNombre in Nombres)
                {
                    if (buscarNombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    {
                        return "el nombre recibido " + nombre + " se encuentra dentro del array";
                    }
                }
                return "Dicho nombre no se encauntra dentro del Array";
            }
            else { 
           return "No se ha recibido ningun nombre ";
            }
        }
    }
}
