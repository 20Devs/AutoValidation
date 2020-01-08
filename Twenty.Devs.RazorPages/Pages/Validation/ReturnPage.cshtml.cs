using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twenty.Devs.RazorPages.Model;

namespace Twenty.Devs.RazorPages
{
    [AutoValidatioFilter(ResultPageKind = ResultPage.ReturnPage,HttpMethod = "post")]
    public class ReturnPageModel : PageModel
    {

        [BindProperty]
        public Profile User { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Fill The Form.";
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Message = "Validation Success";
            return Page();
        }
    }
}