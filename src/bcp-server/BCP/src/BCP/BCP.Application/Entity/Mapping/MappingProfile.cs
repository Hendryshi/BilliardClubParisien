using AutoMapper;
using Common.Application.Extensions;
using BCP.Application.Commands.User.Models;
using BCP.Application.Responses.User;
using BCP.Domain.Entities;

namespace BCP.Application.Entity.Mapping
{

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Entity to Entity (apply changes)
            CreateMap<User, User>().DisableCtorValidation();
            #endregion

            #region Command > Domain
            CreateMap<UserCommand, User>().OptionalValueRules();
            #endregion

            #region Domain > Response
            CreateMap<User, UserResponse>();
            #endregion
        }
    }
}
