using Lama.Api.Data.Models;
using Lama.Api.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lama.Api.Middleware
{
    public class AddUserToDbMiddleware
    {
        private readonly RequestDelegate _next;

        public AddUserToDbMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LamaContext userRepository)
        {
            
            if(context.User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var user = new ApiUser();
                var id = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (!(await userRepository.Users.AnyAsync(x => x.UserId == Guid.Parse(id))))
                {
                    var displayName = context.User.FindFirst(ClaimTypes.GivenName).Value;
                    user.UserId = Guid.Parse(id);
                    user.GivenName = displayName;
                    await userRepository.AddAsync(user);
                }
            }
            await _next(context);
        }
    }
}
