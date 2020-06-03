using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lama.Api.Data.Models
{
    public class ClubMembership
    {
        public Guid ClubMembershipId { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ClubId { get; set; }
        public Club Club { get; set; }
        public Guid UserId { get; set; }
        public ApiUser User { get; set; }
    }
}
