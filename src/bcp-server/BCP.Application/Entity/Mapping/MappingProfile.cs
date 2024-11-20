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
			CreateMap<InscriptionCommand, Domain.Entities.Inscription>()
				.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

			CreateMap<InscriptionImageCommand, Domain.Entities.InscriptionImage>()
				.ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.ImageData != null ? Convert.FromBase64String(src.ImageData) : null));

			CreateMap<Domain.Entities.User, Responses.User.UserResponse>();
			CreateMap<Inscription, Responses.Inscription.InscriptionResponse>();
			CreateMap<InscriptionImage, Responses.Inscription.InscriptionImageResponse>()
				.ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.ImageData != null ?  Convert.ToBase64String(src.ImageData) : null));
		}
	}
}
