using Domain.Models;
using Infrastructure.Data.DataGenerator;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class WowToGoDBContext(DbContextOptions<WowToGoDBContext> options) : DbContext(options)
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        User[] users = UserGenerator.GenerateUsers();
        Event[] events = EventGenerator.GenerateEvents();
        Article[] articles = ArticleGenerator.GenerateArticles();
        Order[] orders = OrderGenerator.GenerateOrders(users);
        OrderItem[] orderItems = OrderItemGenerator.GenerateOrderItems(orders);
        foreach (var order in orders)
        {
            OrderItem[] orderItemsinOrder = orderItems.Where(x => x.OrderId == order.Id).ToArray();
            if (orderItemsinOrder.Length > 0) order.TotalPrice = orderItemsinOrder.Sum(x => x.Price);
        }
        Ticket[] tickets = TicketGenerator.GenerateTickets(events, orders);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Event>().HasData(events);
        modelBuilder.Entity<Article>().HasData(articles);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<OrderItem>().HasData(orderItems);
        modelBuilder.Entity<Ticket>().HasData(tickets);
        modelBuilder.HasDefaultSchema("wowtogo");
    }
}