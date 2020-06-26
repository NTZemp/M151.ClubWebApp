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
        public Task<Club> GetClub(Guid clubId);
        public Task<Club> GetClubByName(string clubName);
        public Task AddInvitation(Guid id, string userName);
    }
}
