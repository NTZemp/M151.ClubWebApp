using Lama.Api.Data.Models;
using Lama.Api.Data.Services.Interfaces;
using Lama.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lama.Api.Data.Services
{
    public class ClubsService : IClubsService
    {
        private readonly LamaContext _context;
        private readonly IUserService _userService;

        public ClubsService(LamaContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Club> Add(string clubName)
        {
            var membership = new ClubMembership {
                User = await _userService.GetLoggedInUserAsync(),
                IsAdmin = true
            };
            var club = new Club {
                ClubName = clubName, 
                Memberships = new List<ClubMembership> 
                { 
                    membership
                } 
            };
            var entity = await _context.AddAsync(club);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<bool> isMember(Guid userId, Guid clubId)
        {
            return await _context.ClubMemberships.AnyAsync(x => x.ClubId == clubId && x.UserId == userId);
        }

        public async Task<bool> isAdmin(Guid userId, Guid clubId)
        {
            return await _context.ClubMemberships.AnyAsync(x => x.ClubId == clubId && x.UserId == userId && x.IsAdmin == true);
        }

        public async Task<Club> GetClub(Guid clubId)
        {
            var currentUser = await _userService.GetLoggedInUserAsync();
            if(await isMember(currentUser.UserId, clubId))
            {
                return await _context.Clubs.Where(c => c.ClubId == clubId)
                    .Include(c => c.Memberships)
                        .ThenInclude(m => m.User)
                    .SingleAsync();
            }
            else
            {
                throw new UnauthorizedException($"Not authorized to access club with id, {clubId}");
            }
        }

        public async Task<Club> GetClubByName(string clubName)
        {
            var club = await _context.Clubs.Where(c => c.ClubName == clubName).SingleAsync();
            var currentUser = await _userService.GetLoggedInUserAsync();
            if (await isMember(currentUser.UserId, club.ClubId))
            {
                return await _context.Clubs.Where(c => c.ClubId == club.ClubId)
                    .Include(c => c.Memberships)
                        .ThenInclude(m => m.User)
                    .SingleAsync();
            }
            else
            {
                throw new UnauthorizedException($"Not authorized to access club with id, {club.ClubId}");
            }
        }

        public async Task<IEnumerable<Club>> GetLoggedInUsersClubs()
        {
            var user = await _userService.GetLoggedInUserAsync();
            var memberships = await _context.ClubMemberships
                .Where(m => m.UserId == user.UserId)
                .Include(m => m.Club)
                .ToListAsync();
            return memberships.Select(x => x.Club);
        }

        public async Task AddInvitation(Guid clubId, string userName)
        {
            var user = await _userService.GetLoggedInUserAsync();
            if(await isAdmin(user.UserId,clubId))
            {
                var invitedUser = await _userService.GetUserByUserNameAsync(userName);
                if(await isMember(clubId, invitedUser.UserId))
                {
                    throw new InvalidOperationException("User is already a member of this club");
                }
                var invitation = new ClubInvitation { ClubId = clubId, UserId = invitedUser.UserId };
                await _context.AddAsync(invitation);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new UnauthorizedException($"Not authorized to invite user to club with id, {clubId}");
            }
        }
    }
}
