using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twenty.Devs.RazorPages.Model;
using Twenty.Devs.RazorPages.Pages.Base;

namespace Twenty.Devs.RazorPages
{
    public class PostValidationModel : BaseValidation
    {

        [BindProperty(SupportsGet = false)]
        public Profile  User                { get; set; }

        public string   Message             { get; set; }

        public void OnGet()
        {
            Message = "Fill the Form ";
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            Message = "Validation Success !...";
            return Page();
        }
    }
}