using System;
using System.Collections.Generic;

namespace Lama.Api.Responses
{
    public class ClubDetailsResponse
    {
        public ClubDetailsResponse()
        {
        }

        public Guid ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubIcon { get; set; }
        public List<UserResponse> Members { get; set; }
    }
}
