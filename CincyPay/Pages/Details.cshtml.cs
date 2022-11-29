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
                if (jsonArray.IsValid(schema, out validationEvents))
                {
                    salaryData = EmpSalary.FromJson(jobDetails);
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
            List<Department> depList = new List<Department>();
          
                Task<string> deptTaskString = deptResult.Content.ReadAsStringAsync();
                string deptJson = deptTaskString.Result;
                depList=Department.FromJson(deptJson);


            List<Department> departments = Department.FromJson(deptJson);
            

            IDictionary<string, Department> filterByDept = new Dictionary<string, Department>();

            foreach (Department department in departments)
            {
                depList.Add(department);

                filterByDept[department.DepartmentCodes] = department;
            }
            List<EmpSalary> FinalFilteredList = new List<EmpSalary>();
            foreach (EmpSalary empSalary in salaryData)
            {
                if (filterByDept.ContainsKey(empSalary.JobCode))
                {
                    FinalFilteredList.Add(empSalary);
                }
            }
            {

            }

            ViewData["EmpSalary"] = FinalFilteredList;
            ViewData["depList"] = depList;


        }
        //private static List<EmpSalary> GetFilteredDeptData()
        //{
        //    var task = client.GetAsync("https://data.cincinnati-oh.gov/resource/wmj4-ygbf.json");
        //    HttpResponseMessage result = task.Result;
        //    List<EmpSalary> salaryData = new List<EmpSalary>();
        //    if (result.IsSuccessStatusCode)
        //    {
        //        Task<string> readString = result.Content.ReadAsStringAsync();
        //        string jobDetails = readString.Result;
        //        JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("empsalary.json"));
        //        JArray jsonArray = JArray.Parse(jobDetails);
        //        IList<string> validationEvents = new List<string>();
        //        if (jsonArray.IsValid(schema, out validationEvents))
        //        {
        //            salaryData = EmpSalary.FromJson(jobDetails);
        //        }
        //        else
        //        {
        //            foreach (string evt in validationEvents)
        //            {
        //                Console.WriteLine(evt);
        //            }

        //        }

        //    }
        //    Task<HttpResponseMessage> deptTask = client.GetAsync("https://data.cincinnati-oh.gov/resource/txnn-6e6x.json");
        //    HttpResponseMessage deptResult = deptTask.Result;
        //    Task<string> deptTaskString = deptResult.Content.ReadAsStringAsync();
        //    string deptJson = deptTaskString.Result;
        //    List<Department> departments = Department.FromJson(deptJson);

        //    IDictionary<string, Department> filterByDept = new Dictionary<string, Department>();
            
        //    foreach (Department department in departments)
        //    {

        //        filterByDept[department.DepartmentCodes] = department;
        //    }
        //    List<EmpSalary> FinalFilteredList = new List<EmpSalary>();
        //    foreach (EmpSalary empSalary in salaryData)
        //    {
        //        if (filterByDept.ContainsKey(empSalary.JobCode))
        //        {
        //            FinalFilteredList.Add(empSalary);
        //        }
        //    }
        //    {

        //    }
        //    return FinalFilteredList;
        //}
    }
}
