using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Twenty.Devs.RazorPages
{
    [ImportModelStateFilter]
    public class ShowValidationResultModel : PageModel
    {


        public void OnGet()
        {

        }
    }
}