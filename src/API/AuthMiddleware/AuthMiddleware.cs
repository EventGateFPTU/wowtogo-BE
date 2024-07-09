using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using UseCases.Common.Models;
using UseCases.Common.Shared;

namespace API.AuthMiddleware;

public class AuthMiddleware(IUnitOfWork unitOfWork, IAuth0Service auth0Service, PipelineContext pipelineContext) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();
        if (authHeader.IsNullOrEmpty() 
            || !authHeader.Contains("Bearer "))
        {
            await next.Invoke(context);
            return;
        }
        authHeader = authHeader.Replace("Bearer ", "");
        
        pipelineContext.Items.Add("JWT", authHeader);
  
        var nameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        if (!context.User.HasClaim(x => x.Type.Equals(nameIdentifier)))
        {
            await next.Invoke(context);
            return;
        }
        
        var subject = context.User.Claims.First(x => x.Type.Equals(nameIdentifier)).Value;
        
        // TODO: can improve
        // Check in db then from Auth0
        var user = await unitOfWork.UserRepository.GetUserBySubject(subject) ?? 
                   await auth0Service.SyncUserProfileAsync(authHeader);
        
        var currentUser = context.RequestServices.GetRequiredService<CurrentUser>();
        currentUser.User = user;
        
        await next.Invoke(context);
    }
}