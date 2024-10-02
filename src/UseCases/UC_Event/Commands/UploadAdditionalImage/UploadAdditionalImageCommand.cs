using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadAdditionalImage;

public record UploadAdditionalImageCommand(
    Guid EventId,
    Stream FileStream) : IRequest<Result>;