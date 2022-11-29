using DepartmentData;
using EmpSalaryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CincyPay.Pages
{
    public class SearchPageModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<SearchPageModel> _logger;

        public SearchPageModel(ILogger<SearchPageModel> logger)
        {
            _logger = logger;
        }

        public SearchPageModel()
        {
        }

        public void OnGet()
        {
            List<EmpSalary> salaryData = GetEmpData();

            ViewData["EmpSalary"] = salaryData;
        }
        public async Task<ApiResponse> GetApiResponseAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.empSalaries = GetEmpData();
            return apiResponse;
        }

        private static List<EmpSalary> GetEmpData()
        {
            var task = client.GetAsync("https://data.cincinnati-oh.gov/resource/wmj4-ygbf.json");
            HttpResponseMessage result = task.Result;
            List<EmpSalary> salaryData = new List<EmpSalary>();
            if (result.IsSuccessStatusCode)
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonString = readString.Result;
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("empsalary.json"));
                JArray jsonArray = JArray.Parse(jsonString);
                IList<string> validationEvents = new List<string>();
                if (jsonArray.IsValid(schema, out validationEvents))
                {
                    salaryData = EmpSalary.FromJson(jsonString);
                }
                else
                {
                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                    }

                }

            }

            return salaryData;
        }
    }
}