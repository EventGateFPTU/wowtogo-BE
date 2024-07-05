using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Organizer.Commands.UploadOrganizerImage;
public record UploadOrganizerImageCommand(Guid OrganizerId, string ImageUrl) : IRequest<Result>;