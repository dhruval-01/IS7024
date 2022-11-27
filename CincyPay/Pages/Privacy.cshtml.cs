using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieData;

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
            var task = client.GetAsync("https://imdb-api.com/en/API/Top250Movies/k_thj97up3");
            HttpResponseMessage result = task.Result;
            Movie movielist = new Movie();
            if (result.IsSuccessStatusCode) 
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonstring = readString.Result;
                movielist = Movie.FromJson(jsonstring);
            }

            ViewData["MovieData"] = movielist;

        }
    }
}