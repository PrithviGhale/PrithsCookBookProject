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
                return View(new List<Recipe>()); // Returns empty list if API call fails
            }
        }
    }
}
