using Domain.Models;
using Infrastructure.Data.DataGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;
public static class WowToGoDbContextSeed
{
    public static async Task Seed(this WowToGoDBContext context)
    {

        try
        {
            await TrySeedAsync(context);
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            throw;
        }
    }

    private static async Task TrySeedAsync(WowToGoDBContext context)
    {
        if ((await context.Set<Category>().Select(c => c.Name).FirstOrDefaultAsync()) is not null) return;
        Article[] articles = ArticleGenerator.GenerateArticles();
        User[] users = UserGenerator.GenerateUsers();
        Organizer[] organizers = OrganizerGenerator.GenerateOrganizers(users);
        Event[] events = EventGenerator.GenerateEvents(organizers);
        Category[] categories = CategoryGenerator.GenerateCategories();
        EventCategory[] eventCategories = EventCategoryGenerator.GenerateEventCategories(events, categories);
        Staff[] staffs = StaffGenerator.GenerateStaff(users, events);
        Show[] shows = ShowGenerator.GenerateShows(events);
        TicketType[] ticketTypes = TicketTypeGenerator.GenerateTicketTypes(events);
        Attendee[] attendees = AttendeeGenerator.GenerateAttendees(users, events);
        Order[] orders = OrderGenerator.GenerateOrders(ticketTypes, users);
        Ticket[] tickets = TicketGenerator.GenerateTickets(attendees, ticketTypes);
        TicketTypeShow[] ticketTypeShows = TicketTypeShowGenerator.GenerateTicketTypeShows(ticketTypes, shows);
        // Seed data
        await context.Articles.AddRangeAsync(articles);
        await context.Users.AddRangeAsync(users);
        await context.Organizers.AddRangeAsync(organizers);
        await context.Events.AddRangeAsync(events);
        await context.Categories.AddRangeAsync(categories);
        await context.EventCategories.AddRangeAsync(eventCategories);
        await context.Staffs.AddRangeAsync(staffs);
        await context.Shows.AddRangeAsync(shows);
        await context.TicketTypes.AddRangeAsync(ticketTypes);
        await context.Attendees.AddRangeAsync(attendees);
        await context.Orders.AddRangeAsync(orders);
        await context.Tickets.AddRangeAsync(tickets);
        await context.TicketTypeShows.AddRangeAsync(ticketTypeShows);
        await context.SaveChangesAsync();
    }
}