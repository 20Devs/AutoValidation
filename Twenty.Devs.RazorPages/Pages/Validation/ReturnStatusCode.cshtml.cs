using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twenty.Devs.RazorPages.Model;

namespace Twenty.Devs.RazorPages
{
    [AutoValidatioFilter
    (
        ResultPageKind              = ResultPage.BadRequest,
        ForHttpMethod               = "get",
        ForHandler                  = "save"
    )]
    public class ReturnStatusCode : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Profile User { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Fill The Form.";
        }

        public IActionResult OnGetSave()
        {
            Message = "Validation Success";
            return Page();
        }

    }
}