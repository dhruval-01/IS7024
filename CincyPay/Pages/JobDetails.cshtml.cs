using EmpSalaryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace CincyPay.Pages
{
    public class JobDetailsModel : PageModel
    {
        public String InputDepid { get; set; }
        public List<EmpSalary> InputJoinList { get; set; }
        public void OnGet(String inputDepName, List<EmpSalary> inputFilteredList)
        {
            InputDepid = inputDepName;
            InputJoinList = inputFilteredList;
            ViewData["DeptList"] = InputJoinList;
        }

    }
}
