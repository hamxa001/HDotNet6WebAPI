using Microsoft.AspNetCore.Mvc.Filters;

namespace HWebAPI.Helpers
{
    public class ActionFileterHelper : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = context.HttpContext.Request.Headers.Authorization;
        }
    }
}
