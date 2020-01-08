using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        ResultPageKind              = ResultPage.RedirectToSamePage, 
        HttpMethod               = "post",
        CopyRouteData               = true,
        CopyModelStateToTempData    = true
    )]
    [AutoValidatioFilter
    (
        ResultPageKind              = ResultPage.ReturnPage,
        HttpMethod               = "get"
    )]
    [ImportModelStateFilter]

    public class RedirectToSamePageModel : PageModel
    {

        [BindProperty]
        public Profile User { get; set; }

        [BindProperty(SupportsGet = true)]
        [Range(10,20,ErrorMessage = "ID Is Incorrect")]
        public int ID { get; set; }

        public string Message { get; set; }


        public void OnGet()
        {
            Message = "Fill The Form.";
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            Message = "Validation Success";
            return Page();
        }

    }
}