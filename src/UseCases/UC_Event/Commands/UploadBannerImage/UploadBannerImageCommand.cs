using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Event.Commands.UploadBannerImage;
public record UploadBannerImageCommand(
    Guid EventId,
    Stream fileStream) : IRequest<Result>;