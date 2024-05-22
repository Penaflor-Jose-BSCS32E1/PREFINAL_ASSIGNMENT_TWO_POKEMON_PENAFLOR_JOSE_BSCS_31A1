using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POKEMONAPP.Models;

namespace POKEMONAPP.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _httpClient;

        public PokemonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon?offset={(page - 1) * 20}&limit=20";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(response);

            ViewBag.HasPrevious = true;
            ViewBag.HasNext = true;
            return View(pokemonList);
        }

        public async Task<IActionResult> Details(string name)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{name}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);

            return View(pokemon);
        }
    }

}   