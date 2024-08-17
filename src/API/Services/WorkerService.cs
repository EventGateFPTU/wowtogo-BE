using Infrastructure.Data;
using MediatR;
using UseCases.UC_Attendees.Commands.SendRemindMailToAttendees;

namespace API.Services;

public class WorkerService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workers = new List<Task>
            {
                DisposeRemindEvent(TimeSpan.FromSeconds(5), stoppingToken)
            };

            await Task.WhenAll(workers.ToArray());
        }
    }

    private async Task DisposeRemindEvent(TimeSpan delay, CancellationToken stoppingToken)
    {
        var now = DateTimeOffset.UtcNow;
        var fromTime = now.AddDays(1);
        var toTime = fromTime.AddSeconds(5);
        Console.WriteLine("worker at: " + now);
        Console.WriteLine("checking shows");
        Console.WriteLine("from: " + fromTime);
        Console.WriteLine("to: " + toTime);
        using var scope = serviceProvider.CreateScope();
        //
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();
        
        await sender.Send(new SendRemindMailToAttendeesCommand(
            FromTime: fromTime,
            ToTime: toTime
        ), cancellationToken: stoppingToken);
        await Task.Delay(delay, stoppingToken);
    }
}