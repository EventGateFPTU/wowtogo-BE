using Ardalis.Result;
using Microsoft.AspNetCore.Routing;
using UseCases.Common.Contracts;

namespace API.Endpoints;
public static class ArticleEndpoints
{
    public static RouteGroupBuilder MapArticleEndpoints(this RouteGroupBuilder group)
    {
        // TODO: remove debugging trash
        group.MapGet("", async (IPermissionManager permissionManager) =>
        {
            var tuple = ("user:cuong", "assignee", "ticket_type:1");
            var tuple1 = ("user:cuong", "assignee", "ticket_type:1");
            var addRes = await permissionManager.PutPermission(tuple, tuple1);
            Console.WriteLine(addRes);

            var checkRes = await permissionManager.HasPermission("user:cuong", "assignee", "ticket_type:1");
            Console.WriteLine(checkRes);

            return Results.Ok();
        });
        return group;
    }
}