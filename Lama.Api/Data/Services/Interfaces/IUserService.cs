using Lama.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lama.Api.Data.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ApiUser> GetUserAsync(Guid userId);
        public Task<ApiUser> GetLoggedInUserAsync();

    }
}
