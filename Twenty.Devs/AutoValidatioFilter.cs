using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Twenty.Devs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AutoValidatioFilter : Attribute, IAsyncPageFilter
    {
        
        public AutoValidatioFilter()
        {
            // You can get the logger by service locator
            // to log every validation error
        }
        #region Properties

        public string           ForHandler                  { get; set; }
        public string           ForHttpMethod               { get; set; }

        public string           Page                        { get; set; }
        public string           Controller                  { get; set; }
        public string           Action                      { get; set; } = "index";
        public bool             CopyRouteData               { get; set; }
        public bool             CopyRouteDataHandler        { get; set; } = false;
        public string           RouteKeys                   { get; set; }
        public bool             CopyModelStateToTempData    { get; set; }
        public string           TempKeyName                 { get; set; } = "ModelState";
        public ResultPage       ResultPageKind              { get; set; } = ResultPage.ReturnPage;
        public int              StatusCodeResult            { get; set; } = 404;         
        #endregion

        #region Methods
        
        protected void                      SetPageResult               (PageHandlerExecutingContext context)
        {
            switch (ResultPageKind)
            {
                case ResultPage.RedirectToSamePage:
                    context.Result = RedirectToSamePage(context);
                    break;
                case ResultPage.RedirectToSpecificPage:
                    context.Result = RedirectToSpecificPage(context);
                    break;
                case ResultPage.RedirectToController:
                    context.Result = RedirectToController(context);
                    break;
                case ResultPage.ReturnPage:
                    context.Result = ReturnPage(context);
                    break;
                case ResultPage.NotFound:
                    context.Result = new NotFoundResult();
                    break;
                case ResultPage.BadRequest:
                    context.Result =  new BadRequestResult();
                    break;
                case ResultPage.EmptyResult:
                    context.Result = new EmptyResult();
                    break;
                case ResultPage.StatusCodeResult:
                    context.Result = new StatusCodeResult(StatusCodeResult);
                    break;
                   
            }

        }

        protected ActionResult              RedirectToSamePage          (PageHandlerExecutingContext context)
        {
            RouteValueDictionary RouteData = null;
            context.RouteData.Values.TryGetValue("page",out var PageRouteItem);

            if (PageRouteItem == null)
                PageRouteItem = context.HttpContext.Request.Path;

            if (this.CopyModelStateToTempData)
                CloneModelStateToTempData(context);

            if (CopyRouteData)
                RouteData = CloneRouteDirectory(context);

            if(CopyRouteData && RouteData!=null )
                return new RedirectToPageResult(PageRouteItem.ToString(),RouteData);

            return new RedirectToPageResult(PageRouteItem.ToString());
        }
        protected ActionResult              RedirectToController        (PageHandlerExecutingContext context)
        {
            RouteValueDictionary RouteData = null;

            if (this.CopyModelStateToTempData)
                CloneModelStateToTempData(context);

            if (CopyRouteData)
                RouteData = CloneRouteDirectory(context);

            if(CopyRouteData && RouteData!=null )
                return new RedirectToActionResult(Action,Controller,RouteData);

            return new RedirectToActionResult(Action,Controller,null);
        }
        protected ActionResult              RedirectToSpecificPage      (PageHandlerExecutingContext context)
        {
            RouteValueDictionary RouteData = null;

            if (this.CopyModelStateToTempData)
                CloneModelStateToTempData(context);

            if (CopyRouteData)
                RouteData = CloneRouteDirectory(context);

            if(CopyRouteData && RouteData!=null )
                return new RedirectToPageResult(Page,RouteData);

            return new RedirectToPageResult(Page);
        }
        protected ActionResult              ReturnPage                  (PageHandlerExecutingContext context)
        {
            if (this.CopyModelStateToTempData)
                CloneModelStateToTempData(context);

            return new PageResult();
        }

        protected void                      CloneModelStateToTempData   (PageHandlerExecutingContext context)
        {
            if(context.HandlerInstance is PageModel item)
                context.ModelState.SaveToTempData(TempKeyName, item.TempData);
        }
        protected RouteValueDictionary      CloneRouteDirectory         (PageHandlerExecutingContext context)
        {
            var dictionaryData = new RouteValueDictionary();
            string[] RouteKeyNames = null;

            var IsRouteKey = !string.IsNullOrWhiteSpace(RouteKeys);
                    
            if (IsRouteKey)
                RouteKeyNames = RouteKeys.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var AnyRouteKey = RouteKeyNames != null && RouteKeyNames.Any();

            foreach (var item in context.HttpContext.Request.Query)
            {
                if (string.Equals(item.Key, "handler", StringComparison.OrdinalIgnoreCase) && !CopyRouteDataHandler)
                    continue;

                if (IsRouteKey)
                {
                    if (AnyRouteKey && RouteKeyNames.Contains(item.Key))
                        dictionaryData.Add(item.Key, item.Value);
                }
                else
                    dictionaryData.Add(item.Key, item.Value);
            }
            return dictionaryData;
        }

        #endregion

        #region Filter Methods
        public async Task   OnPageHandlerExecutionAsync   (PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                // Do Something
                if (!string.IsNullOrWhiteSpace(ForHandler))
                    if (!string.Equals(context.HandlerMethod.Name, ForHandler, StringComparison.InvariantCultureIgnoreCase))
                        await next.Invoke();

                if (!string.IsNullOrWhiteSpace(ForHttpMethod))
                    if (!string.Equals(context.HandlerMethod.HttpMethod, ForHttpMethod, StringComparison.InvariantCultureIgnoreCase))
                        await next.Invoke();

                SetPageResult(context);
            }
            else
                await next.Invoke();
        }
        public       Task   OnPageHandlerSelectionAsync   (PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        } 
        #endregion
    }
}
