using Microsoft.AspNetCore.Mvc.Filters;

public class LoggedInFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext actioncontext, ActionExecutionDelegate next)
    {
        var context = actioncontext.HttpContext;
        AdminService AS = new();
        if(!await AS.IsLoggedIn()){
            Console.WriteLine($"{context.Request.Path} was requested, but the user is not logged in!");
            context.Response.StatusCode = 401;
            return;
        }
        await next();
    }
}