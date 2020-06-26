using Lama.Api.Data.Models;
using Lama.Api.Data.Services.Interfaces;
using Lama.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lama.Api.Data.Services
{
    public class UserService : IUserService
    {
        private readonly LamaContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(LamaContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ClubInvitation>> GetInvitations()
        {
            var user = await GetLoggedInUserAsync();
            return await _context.ClubInvitations.Where(e => e.UserId == user.UserId)
                .Include(e => e.Club)
                .ToListAsync();
        }

        public async Task UpdateInvitationAsync(Guid invitationId, InvitationStatus invitationStatus)
        {
            var user = await GetLoggedInUserAsync();
            var invitation = await _context.FindAsync<ClubInvitation>(invitationId);
            if(invitation.UserId == user.UserId)
            {
                if(invitationStatus == InvitationStatus.Accept)
                {
                    await AcceptInvitationAsync(invitation, user.UserId);
                }
                else if(invitationStatus == InvitationStatus.Reject)
                {
                    await RejectInvitationAsync(invitation);
                }
            }
            else
            {
                throw new UnauthorizedException("User is not allowed to access invitation");
            }
        }

        private async Task AcceptInvitationAsync(ClubInvitation invitation, Guid userId)
        {
            var membership = new ClubMembership { UserId = userId, ClubId = invitation.ClubId };
            await _context.AddAsync(membership);
            _context.Remove(invitation);
            await _context.SaveChangesAsync();
        }

        private async Task RejectInvitationAsync(ClubInvitation invitation)
        {

            _context.Remove(invitation);
            await _context.SaveChangesAsync();
        }

        public async Task<ApiUser> GetLoggedInUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _context.Users.FindAsync(Guid.Parse(userId));
        }

        public async Task<ApiUser> GetUserAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<ApiUser> GetUserByUserNameAsync(string userName)
        {
            try
            {
                return await _context.Users.Where(u => u.UserName == userName).SingleAsync();
            }
            catch (System.InvalidOperationException)
            {
                throw new KeyNotFoundException($"The user with username:{userName} couldn't be found");
            }
        }
    }
}
