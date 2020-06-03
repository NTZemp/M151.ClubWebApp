using System;
using System.Collections.Generic;
using System.Text;

namespace Lama.Api.Data.Models
{
    public class ApiUser
    {
        public Guid UserId { get; set; }
        public string GivenName { get; set; }
        public virtual ICollection<ClubMembership> Memberships { get; set; }

    }
}
