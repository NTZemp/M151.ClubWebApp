using System;
using System.Collections.Generic;

namespace Lama.Api.Responses
{
    public class ClubResponse
    {
        public ClubResponse()
        {
            
        }

        public Guid ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubIcon { get; set; }
        //public List<UserResponse> Users { get; set; }
    }
}
