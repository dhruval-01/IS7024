using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DepartmentData;
using EmpSalaryData;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CincyPay.Pages
{
    public class DetailsModel : PageModel
    {

        static readonly HttpClient client = new HttpClient();
        public void OnGet()
        {
            List<EmpSalary> FinalFilteredList = GetFilteredDeptData();

            ViewData["EmpSalary"] = FinalFilteredList;
        }

        private static List<EmpSalary> GetFilteredDeptData()
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

            Task<HttpResponseMessage> deptTask = client.GetAsync("https://data.cincinnati-oh.gov/resource/txnn-6e6x.json");
            HttpResponseMessage deptResult = deptTask.Result;
            Task<string> deptTaskString = deptResult.Content.ReadAsStringAsync();
            string deptJson = deptTaskString.Result;
            List<Department> departments = Department.FromJson(deptJson);

            IDictionary<string, Department> filterByDept = new Dictionary<string, Department>();
            foreach (Department department in departments)
            {
                filterByDept[department.DepartmentCodes] = department;
            }
            List<EmpSalary> FinalFilteredList = new List<EmpSalary>();
            foreach (EmpSalary empSalary in salaryData)
            {
                if (filterByDept.ContainsKey(empSalary.Jobcode))
                {
                    FinalFilteredList.Add(empSalary);
                }
            }
            {

            }

            return FinalFilteredList;
        }
    }
}