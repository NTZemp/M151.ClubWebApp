using Lama.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lama.Api.Data.Services.Interfaces
{
    public interface IClubsService
    {
        public Task<IEnumerable<Club>> GetLoggedInUsersClubs();
        public Task<Club> Add(string clubName);
        public Task<Club> AddMember(Guid clubId, ApiUser user);
        public Task<Club> GetClub(Guid clubId);
        public Task<Club> GetClubByName(string clubName);
    }
}
