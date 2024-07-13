using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadBackgroundImage;
public record UploadBackgroundImageCommand(
    Guid EventId,
    Stream fileStream) : IRequest<Result>;