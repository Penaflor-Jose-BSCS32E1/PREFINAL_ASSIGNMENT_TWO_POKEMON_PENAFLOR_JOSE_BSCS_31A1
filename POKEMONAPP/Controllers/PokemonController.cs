using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POKEMONAPP.Models;
using Microsoft.Extensions.Logging;
using Serilog;

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
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon?offset={(page - 1) * 20}&limit=20";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(response);

            ViewBag.HasPrevious = true;
            ViewBag.HasNext = true;
            return View(pokemonList);
        }

        public async Task<IActionResult> Details(string name)
        {
            try
            {
                // Example URL for fetching Pokemon details from an API (replace with your actual API endpoint)
                string apiUrl = $"https://api.example.com/pokemon/{name}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var pokemonDetails = JsonConvert.DeserializeObject<PokemonResult>(responseContent);
                    return Ok(pokemonDetails);
                }
                else
                {
                    // Log error for unsuccessful response
                    _logger.LogError("Failed to fetch Pokemon details. Status code: {StatusCode}", response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Failed to fetch Pokemon details.");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions
                _logger.LogError(ex, "An error occurred while fetching Pokemon details.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}


