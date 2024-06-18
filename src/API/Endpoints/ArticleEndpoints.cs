using Microsoft.AspNetCore.Routing;
namespace API.Endpoints;
public static class ArticleEndpoints
{
    public static RouteGroupBuilder MapArticleEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("", () => "hello articles");
        return group;
    }
}