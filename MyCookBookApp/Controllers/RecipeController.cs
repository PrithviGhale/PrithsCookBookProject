using System.Text;
using System.Text.Json;
using MyCookBookApp.Models; 
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyCookBookApp.Models;
using System.Collections.Generic;
using System.Text;

namespace MyCookBookApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7238"; // Base URL for the API

        public RecipeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Display all recipes
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/api/Recipe");
                var recipes = JsonSerializer.Deserialize<List<Recipe>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(recipes ?? new List<Recipe>());
            }
            catch
            {
                return View(new List<Recipe>());
            }
        }

        // Search recipes
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                var searchRequest = new RecipeSearchRequest { Keyword = query };
                var content = new StringContent(JsonSerializer.Serialize(searchRequest), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/Recipe/search", content);
                if (!response.IsSuccessStatusCode) return View("Index", new List<Recipe>());

                var responseData = await response.Content.ReadAsStringAsync();
                var recipes = JsonSerializer.Deserialize<List<Recipe>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
                var content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/Recipe", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

        // Delete a recipe
        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/Recipe/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

        // Edit a recipe
        [HttpPost]
        public async Task<IActionResult> EditRecipe(Recipe recipe)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/Recipe/{recipe.RecipeId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
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
