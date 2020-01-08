using System;
using System.Collections.Generic;
using System.Text;

namespace Twenty.Devs
{
    public enum ResultPage
    {
        RedirectToSamePage,
        RedirectToSpecificPage,
        RedirectToController,
        ReturnPage,
        NotFound,
        BadRequest,
        EmptyResult,
        StatusCodeResult,
    }
}
