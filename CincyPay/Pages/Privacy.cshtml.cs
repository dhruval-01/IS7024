using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipes;

namespace CincyPay.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        static readonly HttpClient client = new HttpClient();

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var task = client.GetAsync("https://nutritionalrecipes20221126140429.azurewebsites.net/NutritionalRecipes/v2?foodItem=biryani");
            HttpResponseMessage result = task.Result;
            RecipeClass recipeList = new RecipeClass();
            if (result.IsSuccessStatusCode) 
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonstring = readString.Result;
                recipeList = RecipeClass.FromJson(jsonstring);
            }

            ViewData["RecipeData"] = recipeList;

        }
    }
}