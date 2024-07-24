using Ardalis.Result;
using MediatR;
using UseCases.UC_Staff.Queries.GetCheckinInfo;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.StaffEndpointHandler.Queries;

public class GetCheckinInfoEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender, CheckinInfoRequest infoRequest, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetCheckinInfoQuery(infoRequest.Code, infoRequest.ShowId), cancellationToken);
        if (result.IsSuccess) return Results.Ok(result);
        return result.Status switch
        {
            ResultStatus.NotFound => Results.NotFound(result),
            ResultStatus.Forbidden => Results.Forbid(),
            _ => Results.BadRequest(result)
        };
    }
    public record CheckinInfoRequest(Guid ShowId, string Code);
}