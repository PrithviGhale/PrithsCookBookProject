using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyCookBookApp.Models;
using System.Collections.Generic;

namespace MyCookBookApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly HttpClient _httpClient;

        public RecipeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Display all recipes
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiBaseUrl = "https://localhost:7238";
                var response = await _httpClient.GetStringAsync($"{apiBaseUrl}/api/Recipe");

                var recipes = JsonSerializer.Deserialize<List<Recipe>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(recipes ?? new List<Recipe>());
            }
            catch
            {
                return View(new List<Recipe>());
            }
        }

        // Search for recipes
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                var apiBaseUrl = "https://localhost:7238";
                var response = await _httpClient.GetStringAsync($"{apiBaseUrl}/api/Recipe/search?query={query}");

                var recipes = JsonSerializer.Deserialize<List<Recipe>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View("Index", recipes ?? new List<Recipe>());
            }
            catch
            {
                return View("Index", new List<Recipe>());
            }
        }

        // Add a new recipe
        [HttpPost]
        public async Task<IActionResult> AddRecipe(Recipe recipe)
        {
            try
            {
                var apiBaseUrl = "https://localhost:7238";
                var content = new StringContent(JsonSerializer.Serialize(recipe), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{apiBaseUrl}/api/Recipe", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }
    }
}