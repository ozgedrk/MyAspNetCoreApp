using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action method calismadan once");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action method calistiktan sonra");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("Action method sonuc uretilmeden once");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("Action method  sonuc uretildikten sonra");
        }
    }
}
