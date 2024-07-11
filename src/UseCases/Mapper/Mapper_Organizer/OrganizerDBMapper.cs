using Domain.Models;
using Domain.Responses.Responses_Organizer;

namespace UseCases.Mapper.Mapper_Organizer
{
	public static class OrganizerDBMapper
	{
		public static OrganizerDB MapOrganizerDB(this Organizer organizer)
			=> new OrganizerDB(organizer.Id, organizer.OrganizationName, organizer.Description, organizer.ImageUrl);
	}
}
