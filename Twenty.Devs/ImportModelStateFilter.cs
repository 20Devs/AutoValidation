using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Twenty.Devs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ImportModelStateFilter : Attribute, IAsyncPageFilter
    {
        public string   ForHandler        { get; set; }
        public string   ForHttpMethod     { get; set; }
        public string   TempKeyName       { get; set; } = "ModelState";


        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (!string.IsNullOrWhiteSpace(ForHandler))
                if (!string.Equals(context.HandlerMethod.Name, ForHandler, StringComparison.InvariantCultureIgnoreCase))
                    await next.Invoke();

            if (!string.IsNullOrWhiteSpace(ForHttpMethod))
                if (!string.Equals(context.HandlerMethod.HttpMethod, ForHttpMethod, StringComparison.InvariantCultureIgnoreCase))
                    await next.Invoke();

            if (context.HandlerInstance is PageModel item)
                context.ModelState.RetrieveFromTempData(TempKeyName, item.TempData);

            await next.Invoke();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }
    }
}
