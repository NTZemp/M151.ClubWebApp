using Lama.Api.Data.Models;
using Lama.Api.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

        public async Task<ApiUser> GetLoggedInUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _context.Users.FindAsync(Guid.Parse(userId));
        }

        public async Task<ApiUser> GetUserAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
