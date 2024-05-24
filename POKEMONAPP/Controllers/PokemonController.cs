using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POKEMONAPP.Models;
using Microsoft.Extensions.Logging;
using Serilog;
using Newtonsoft.Json.Linq;

namespace POKEMONAPP.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokemonController> _logger;
        public PokemonController(ILogger<PokemonController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon?offset={(page - 1) * 30}&limit=30";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(response);

            ViewBag.HasPrevious = true;
            ViewBag.HasNext = true;
            return View(pokemonList);
        }

        public async Task<IActionResult> Details(string name)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            var data = JObject.Parse(response);

            var pokemon = new Pokemon
            {
                Name = data["name"].ToString(),
                Moves = data["moves"].Select(m => m["move"]["name"].ToString()).ToList(),
                Abilities = data["abilities"].Select(a => a["ability"]["name"].ToString()).ToList()
            };

            return View(pokemon);
        }
    }
}


