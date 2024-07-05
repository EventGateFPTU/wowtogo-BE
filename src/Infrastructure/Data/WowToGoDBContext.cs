using Domain.Models;
using Infrastructure.Data.DataGenerator;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class WowToGoDBContext(DbContextOptions<WowToGoDBContext> options) : DbContext(options)
{
    public DbSet<TicketTypeShow> TicketTypeShows => Set<TicketTypeShow>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Attendee> Attendees => Set<Attendee>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<EventCategory> EventCategories => Set<EventCategory>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Organizer> Organizers => Set<Organizer>();
    public DbSet<Show> Shows => Set<Show>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketType> TicketTypes => Set<TicketType>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("wowtogo");
        // Seed data
        Article[] articles = ArticleGenerator.GenerateArticles();
        User[] users = UserGenerator.GenerateUsers();
        Organizer[] organizers = OrganizerGenerator.GenerateOrganizers(users);
        Event[] events = EventGenerator.GenerateEvents(organizers);
        Category[] categories = CategoryGenerator.GenerateCategories();
        EventCategory[] eventCategories = EventCategoryGenerator.GenerateEventCategories(events, categories);
        Staff[] staffs = StaffGenerator.GenerateStaff(users, events);
        Show[] shows = ShowGenerator.GenerateShows(events);
        TicketType[] ticketTypes = TicketTypeGenerator.GenerateTicketTypes(shows);
        Attendee[] attendees = AttendeeGenerator.GenerateAttendees(users, events);
        Order[] orders = OrderGenerator.GenerateOrders(ticketTypes, users);
        Ticket[] tickets = TicketGenerator.GenerateTickets(attendees, ticketTypes);
        TicketTypeShow[] ticketTypeShows = TicketTypeShowGenerator.GenerateTicketTypeShows(ticketTypes, shows);
        // Seed data
        modelBuilder.Entity<Article>().HasData(articles);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Organizer>().HasData(organizers);
        modelBuilder.Entity<Event>().HasData(events);
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<EventCategory>().HasData(eventCategories);
        modelBuilder.Entity<Staff>().HasData(staffs);
        modelBuilder.Entity<Show>().HasData(shows);
        modelBuilder.Entity<TicketType>().HasData(ticketTypes);
        modelBuilder.Entity<Attendee>().HasData(attendees);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<Ticket>().HasData(tickets);
        modelBuilder.Entity<TicketTypeShow>().HasData(ticketTypeShows);
    }
}