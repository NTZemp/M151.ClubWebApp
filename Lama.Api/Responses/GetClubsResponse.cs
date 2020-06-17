using System;
using System.Collections.Generic;

namespace Lama.Api.Responses
{
    public class GetClubsResponse
    {
        public GetClubsResponse()
        {
            
        }
        public List<ClubResponse> Clubs { get; set; }
    }
}
