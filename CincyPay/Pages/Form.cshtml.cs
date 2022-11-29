using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShortCutspace;    

namespace CincyPay.Pages
{
    public class FormModel : PageModel
    {
        private readonly ILogger<FormModel> _logger;

        [BindProperty]
        public Shortcut shortcut { get; set; }

        public FormModel(ILogger<FormModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            ViewData["ShortcutList"] = ShortcutRoster.allShortcuts;
        }

        public void OnPost()
        {
            // update the local map. 
            string stuff = shortcut.FirstName + shortcut.LastName + shortcut.Email + shortcut.Interests;
            ShortcutRoster.allShortcuts.Add(shortcut);

            ViewData["ShortcutList"] = ShortcutRoster.allShortcuts;
        }
    }
}
