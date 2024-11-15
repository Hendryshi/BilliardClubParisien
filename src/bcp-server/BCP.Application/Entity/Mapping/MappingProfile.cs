using AutoMapper;
using BCP.Application.Commands.Inscription.Models;
using BCP.Domain.Entities;
using System.Security.AccessControl;

namespace BCP.Application.Entity.Mapping
{
	public sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Commands.User.Models.UserCommand, Domain.Entities.User>();
			CreateMap<Commands.Inscription.Models.InscriptionCommand, Domain.Entities.Inscription>();

			CreateMap<Domain.Entities.User, Responses.User.UserResponse>();
			CreateMap<Inscription, Responses.Inscription.InscriptionResponse>();

		}
	}
}
