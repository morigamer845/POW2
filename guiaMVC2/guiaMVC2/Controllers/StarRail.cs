using guiaMVC2.Models;
using guiaMVC2.services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace guiaMVC2.Controllers
{
    public class StarRail : Controller
    {
        private readonly HSRServices _hsrServices;

        public StarRail()
        {
            _hsrServices = new HSRServices();
        }

        public async Task<IActionResult> Character()
        {
            var personajes = await _hsrServices.GetCharactersAsync();
            System.IO.File.WriteAllText("wwwroot/data/characters.json",
              Newtonsoft.Json.JsonConvert.SerializeObject(personajes, Formatting.Indented));

            // Leer el archivo JSON desde la carpeta wwwroot/data
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "characterimagen.json");

            var jsonData = System.IO.File.ReadAllText(jsonPath);
            var imageMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

            foreach (var c in personajes)
            {
                if (imageMap.ContainsKey(c.Name))
                {
                    c.LocalImage = imageMap[c.Name];
                }
                else
                {
                    c.LocalImage = "/images/characters/default.png"; // fallback si no existe
                }
            }
            return View(personajes);

        }

        public async Task<IActionResult> find(string Name)
        {
         
            var personajes = await _hsrServices.GetCharactersAsync();
       
                // Filtro por coincidencia parcial (ignora mayúsculas/minúsculas)
                var resultado = personajes
                    .Where(p => p.Name.Contains(Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Si querés reutilizar la misma vista "Character.cshtml"
                return View("Character", resultado);
            
        }

        public async Task<IActionResult> Details(int Id)
        {
            // Obtenemos la lista completa de personajes desde el servicio
            var personajes = await _hsrServices.GetCharactersAsync();

            // Leer el JSON de imágenes como hiciste en Character()
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "characterimagen.json");
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            var imageMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

            foreach (var c in personajes)
            {
                c.LocalImage = imageMap.ContainsKey(c.Name) ? imageMap[c.Name] : "/images/characters/default.png";
            }

            // Buscar el personaje por Id
            var personaje = personajes.FirstOrDefault(p => p.Id == Id);

            if (personaje == null)
            {
                return NotFound(); // Retorna 404 si no existe
            }

            // Retornar la vista con el personaje como modelo
            return View(personaje);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
