using System;
namespace Lama.Api.Data.Models
{
    public class ClubInvitation
    {
        public ClubInvitation()
        {
        }

        public Guid InvitationId { get; set; }
        public Guid ClubId { get; set; }
        public Guid UserId { get; set; }

        public ApiUser User { get; set; }
        public Club Club { get; set; }
    }
}
