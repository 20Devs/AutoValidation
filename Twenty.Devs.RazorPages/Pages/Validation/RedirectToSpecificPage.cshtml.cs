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
    [AutoValidatioFilter
    (
        ResultPageKind              = ResultPage.RedirectToSpecificPage,
        HttpMethod               = "get",
        Handler                  = "save",
        CopyModelStateToTempData    = true,
        Page                        = "ShowValidationResult"
    )]
    public class RedirectToSpecificPageModel : PageModel
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