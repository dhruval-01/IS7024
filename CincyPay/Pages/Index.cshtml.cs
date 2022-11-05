using EmpSalaryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CincyPay.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var task = client.GetAsync("https://data.cincinnati-oh.gov/resource/wmj4-ygbf.json");
            HttpResponseMessage result = task.Result;
            List<EmpSalary> salaryData = new List<EmpSalary>();
            if (result.IsSuccessStatusCode) 
            { 
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonString = readString.Result;
                salaryData = EmpSalary.FromJson(jsonString);
            }

            ViewData["EmpSalary"] = salaryData;

        }
    }
}