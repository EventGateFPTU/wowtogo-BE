using API.Authorization;

namespace API.AuthMiddleware;

public class AuthMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        
        var nameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        if (context.User.HasClaim(x => x.Type.Equals(nameIdentifier)))
        {
            var id = context.User.Claims.First(x => x.Type.Equals(nameIdentifier)).Value;
            var currentUser = context.RequestServices.GetRequiredService<CurrentUser>();
            currentUser.Id = id;
        }
        
        await next.Invoke(context);
    }
}