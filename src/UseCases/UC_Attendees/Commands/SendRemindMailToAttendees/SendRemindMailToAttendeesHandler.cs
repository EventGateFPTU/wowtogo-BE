using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Interfaces.Email;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UseCases.UC_Attendees.Commands.SendRemindMailToAttendees;

public class SendRemindMailToAttendeesHandler(IUnitOfWork unitOfWork, IMailService mailService) : IRequestHandler<SendRemindMailToAttendeesCommand, Result>
{
    public async Task<Result> Handle(SendRemindMailToAttendeesCommand request, CancellationToken cancellationToken)
    {
        var query = await unitOfWork.TicketTypeShowRepository.DBSet()
            .Include(tts => tts.Show)
            .Where(tts => tts.Show.StartsAt.LocalDateTime > request.FromTime
                          && tts.Show.StartsAt.LocalDateTime > request.ToTime)
            .Include(tts => tts.TicketType)
            .ThenInclude(tt => tt.Tickets)
            .ThenInclude(t => t.Attendee)
            .ToListAsync(cancellationToken: cancellationToken);
 
        var shows = query.Select(tts => tts.Show);
        
        var isSuccess = true;
        foreach (var show in shows)
        {
            var attendees = query
                .Where(tts => tts.Show.Id.Equals(show.Id))
                .SelectMany(s => s.TicketType.Tickets)
                .Select(t => t.Attendee);
            
            var emails = attendees
                .Select(a => a.Email)
                .ToArray();
            
            // TODO: Log out if failed to send email
            if (emails.Length == 0)
                isSuccess &= await mailService.SendReminderMainAsync(emails, show.Title, show.StartsAt.ToString());
        }
        
        return isSuccess ? Result.Success() : Result.Error();
    }
}