using EmpSalaryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ShortCutspace;
/*namespace Roster19FS7024.Pages;*/
namespace CincyPay.Pages;
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public ShortCutspace.Shortcut shortcut { get; set; }

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
                string jobDetails = readString.Result;
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("empsalary.json"));
                JArray jsonArray = JArray.Parse(jobDetails);
                IList<string> validationEvents = new List<string>();
                if(jsonArray.IsValid(schema, out validationEvents))
                {
                    salaryData = EmpSalary.FromJson(jobDetails);
                } else {
                    foreach(string evt in validationEvents) {
                        Console.WriteLine(evt);
                    }
                       
                }
                
            }

            ViewData["EmpSalary"] = salaryData;
            ViewData["ShortcutList"] = ShortCutspace.ShortcutRoster.allShortcuts;

        }

    public void OnPost()
    {
        // update the local map. 
        string stuff = shortcut.FirstName + shortcut.LastName + shortcut.Email + shortcut.Interests;
        ShortcutRoster.allShortcuts.Add(shortcut);

        ViewData["ShortcutList"] = ShortcutRoster.allShortcuts;
    }
}
