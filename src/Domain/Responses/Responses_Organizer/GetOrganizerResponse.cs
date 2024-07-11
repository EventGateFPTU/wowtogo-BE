namespace Domain.Responses.Responses_Organizer
{
	public record OrganizerDB(Guid Id, string OrganizationName, string Description, string ImageUrl);
	public record class GetAllOrganizerResponse(int PageNumber, int PageSize, IEnumerable<OrganizerDB> Organizer);
}
