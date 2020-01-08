using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Twenty.Devs.RazorPages.Pages.Base
{
    [AutoValidatioFilter(ForHttpMethod = "post",ResultPageKind = ResultPage.ReturnPage)]
    public class BaseValidation : PageModel
    {

    }
}
