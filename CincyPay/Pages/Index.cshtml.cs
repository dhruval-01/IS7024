using EmpSalaryData;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
                string DepartmentDetails = readString.Result;
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("empsalary.json"));
                JArray jsonArray = JArray.Parse(DepartmentDetails);
                IList<string> validationEvents = new List<string>();
                if(jsonArray.IsValid(schema, out validationEvents))
                {
                    salaryData = EmpSalary.FromJson(DepartmentDetails);
                } else {
                    foreach(string evt in validationEvents) {
                        Console.WriteLine(evt);
                    }
                       
                }
                
            }

            ViewData["EmpSalary"] = salaryData;

        }
    }
}