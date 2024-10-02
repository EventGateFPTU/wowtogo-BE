using Domain.Models;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class WowToGoDBContext(DbContextOptions<WowToGoDBContext> options, IMediator mediator) : DbContext(options)
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
    public DbSet<ShowStaff> ShowStaffs => Set<ShowStaff>();
    public DbSet<Checkin> Checkins => Set<Checkin>();
    public DbSet<AdditionalImage> AdditionalImages => Set<AdditionalImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("wowtogo");

        modelBuilder.Entity<User>().HasIndex(b => b.Subject).IsUnique();

        // // Seed data

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}