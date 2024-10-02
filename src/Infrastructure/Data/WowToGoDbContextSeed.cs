using Domain.Models;
using Infrastructure.Data.DataGenerator;
using Microsoft.EntityFrameworkCore;

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
        Checkin[] checkins = CheckinGenerator.GenerateCheckins(shows, tickets);
        AdditionalImage[] additionalImages = AdditionalImageGenerator.Generate(events);
        // Seed data
        IList<Task> tasks = [];
        tasks.Add(context.Articles.AddRangeAsync(articles));
        tasks.Add(context.Users.AddRangeAsync(users));
        tasks.Add(context.Organizers.AddRangeAsync(organizers));
        tasks.Add(context.Events.AddRangeAsync(events));
        tasks.Add(context.Categories.AddRangeAsync(categories));
        tasks.Add(context.EventCategories.AddRangeAsync(eventCategories));
        tasks.Add(context.Staffs.AddRangeAsync(staffs));
        tasks.Add(context.Shows.AddRangeAsync(shows));
        tasks.Add(context.TicketTypes.AddRangeAsync(ticketTypes));
        tasks.Add(context.Attendees.AddRangeAsync(attendees));
        tasks.Add(context.Orders.AddRangeAsync(orders));
        tasks.Add(context.Tickets.AddRangeAsync(tickets));
        tasks.Add(context.TicketTypeShows.AddRangeAsync(ticketTypeShows));
        tasks.Add(context.Checkins.AddRangeAsync(checkins));
        tasks.Add(context.AdditionalImages.AddRangeAsync(additionalImages));
        await Task.WhenAll(tasks);
        await context.SaveChangesAsync();
    }
}