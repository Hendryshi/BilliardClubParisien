using AutoMapper;
using Common.Application.Extensions;
using BCP.Application.Commands.User.Models;
using BCP.Application.Responses.User;
using BCP.Domain.Entities;
using BCP.Application.Commands.Inscription.Models;

namespace BCP.Application.Entity.Mapping
{

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Entity to Entity (apply changes)
            CreateMap<User, User>().DisableCtorValidation();
            CreateMap<Inscription, Inscription>().DisableCtorValidation();
            #endregion

            #region Command > Domain
            CreateMap<UserCommand, User>().OptionalValueRules();
            CreateMap<InscriptionCommand, Inscription>().OptionalValueRules();
            CreateMap<InscriptionImageCommand, Domain.Entities.InscriptionImage>()
                .OptionalValueRules(nameof(InscriptionImage.ImageData)) // Ìø¹ý ImageData
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src =>src.ImageData.HasValue ? Convert.FromBase64String(src.ImageData.Value) : null));
            #endregion

            #region Domain > Response
            CreateMap<User, UserResponse>();
            CreateMap<Inscription, Responses.Inscription.InscriptionResponse>();
            CreateMap<InscriptionImage, Responses.Inscription.InscriptionImageResponse>()
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.ImageData != null ? Convert.ToBase64String(src.ImageData) : null));
            #endregion
        }
    }
}
