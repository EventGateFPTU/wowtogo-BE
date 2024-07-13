using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Organizer.Commands.UploadOrganizerImage;
public record UploadOrganizerImageCommand(Stream imageStream) : IRequest<Result>;