using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lama.Api.Data.Models
{
    public partial class Club
    {
        public Guid ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubIcon { get; set; }
        public virtual ICollection<ClubMembership> Memberships { get; set; }
    }
}
