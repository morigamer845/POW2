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
        private void MapImagesToCharacters(List<Personaje> personajes)
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "characterimagen.json");
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            var imageMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

            foreach (var c in personajes)
            {
                c.LocalImage = imageMap.ContainsKey(c.Name)
                    ? imageMap[c.Name]
                    : "/images/characters/default.png";
            }
        }

        public async Task<IActionResult> Character()
        {
            var personajes = await _hsrServices.GetCharactersAsync();

            // Guardar copia en JSON
            System.IO.File.WriteAllText("wwwroot/data/characters.json",
                Newtonsoft.Json.JsonConvert.SerializeObject(personajes, Formatting.Indented));

            // Asignar imágenes
            MapImagesToCharacters(personajes);

            return View(personajes);
        }

        public async Task<IActionResult> Find(string Name)
        {
            var personajes = await _hsrServices.GetCharactersAsync();

            var resultado = string.IsNullOrWhiteSpace(Name)
                ? personajes
                : personajes
                    .Where(p => p.Name.Contains(Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // 🔹 Importante: asignar imágenes a los resultados filtrados
            MapImagesToCharacters(resultado);

            return View("Character", resultado);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var personajes = await _hsrServices.GetCharactersAsync();

            // 🔹 Asignar imágenes
            MapImagesToCharacters(personajes);

            var personaje = personajes.FirstOrDefault(p => p.Id == Id);

            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
