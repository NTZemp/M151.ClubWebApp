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
        }
    }
}
