using EmpSalaryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace CincyPay.Pages
{
    public class JobDetailsModel : PageModel
    {
        public String inputDepid { get; set; }
        public List<EmpSalary> inputJoinList { get; set; }
        public void OnGet(String inputDepName, List<EmpSalary> inputFilteredList)
        {
            inputDepid = inputDepName;
            inputJoinList = inputFilteredList;
            ViewData["DeptList"] = inputJoinList;
        }

    }
}
