using AutoMapper;

namespace BCP.Application.Entity.Mapping
{
	public sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Commands.User.Models.UserCommand, Domain.Entities.User>();

			CreateMap<Domain.Entities.User, Responses.User.UserResponse>();
		}
	}
}
