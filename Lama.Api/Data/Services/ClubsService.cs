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

        public async Task<Club> AddMember(Guid clubId,ApiUser user)
        {
            var membership = new ClubMembership { User = user };
            var club = await _context.Clubs.FindAsync(clubId);
            var currentUser = await _userService.GetLoggedInUserAsync();
            var currentMembership = await _context.ClubMemberships.Where(x => 
                x.UserId == currentUser.UserId && x.ClubId == clubId
            ).SingleOrDefaultAsync();
            if (await isMember(currentUser.UserId,clubId) && currentMembership.IsAdmin)
            {
                club.Memberships.Add(membership);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedException($"Unauthorized to add to club with id, {clubId}");
            }
            return club;
        }

        public async Task<bool> isMember(Guid userId, Guid clubId)
        {
            return await _context.ClubMemberships.AnyAsync(x => x.ClubId == clubId && x.UserId == userId);
        }

        public async Task<Club> GetClub(Guid clubId)
        {
            var currentUser = await _userService.GetLoggedInUserAsync();
            if(await isMember(currentUser.UserId, clubId))
            {
                return await _context.Clubs.FindAsync(clubId);
            }
            else
            {
                throw new UnauthorizedException($"Not authorized to access club with id, {clubId}");
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
    }
}
