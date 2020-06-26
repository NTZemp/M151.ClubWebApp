using System;
using AutoMapper;
using Lama.Api.Data.Models;
using Lama.Api.Responses;

namespace Lama.Api.Mappings
{
    public class ClubMapping : Profile
    {
        public ClubMapping()
        {
            CreateMap<Club, ClubResponse>();
            CreateMap<ClubMembership, UserResponse>()
                .ForMember(u => u.UserId, opt => opt.MapFrom(source => source.UserId))
                .ForMember(u => u.Name, opt => opt.MapFrom(source => source.User.GivenName));
            CreateMap<Club, ClubDetailsResponse>()
                .ForMember(e => e.Members, opt => opt.MapFrom(c => c.Memberships));
            CreateMap<ClubInvitation, InvitationResponse>()
                .ForMember(e => e.ClubName, opt => opt.MapFrom(c => c.Club.ClubName));

        }
    }
}
