using API.Endpoints;
using Npgsql.Replication;

namespace API.Middlewares;
public static class MinimalAPIConfig
{
    public static IApplicationBuilder MapWowToGoEndpoints(this WebApplication app)
    {
        app.MapGroup("/articles").MapArticleEndpoints().WithTags("Articles");
        app.MapGroup("/events").MapEventEndpoints().WithTags("Events");
        app.MapGroup("/orders").MapOrderEndpoints().WithTags("Orders");
        app.MapGroup("/tickets").MapTicketEndpoints().WithTags("Tickets");
        app.MapGroup("/users").MapUserEndpoints().WithTags("Users");
        app.MapGroup("/categories").MapCategoryEndpoints().WithTags("Categories");
        app.MapGroup("/staff").MapStaffEndpoints().WithTags("Staff");
        app.MapGroup("/ticket-types").MapTicketTypeEndpoints().WithTags("Ticket Types");
        app.MapGroup("/shows").MapShowEndpoints().WithTags("Shows");
        app.MapGroup("/organizers").MapOrganizerEndpoints().WithTags("Organizers");
        return app;
    }
}