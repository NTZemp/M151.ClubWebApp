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

        public async Task InvokeAsync(HttpContext context, LamaContext lamaContext)
        {
            
            if(context.User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var user = new ApiUser();
                var id = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (!(await lamaContext.Users.AnyAsync(x => x.UserId == Guid.Parse(id))))
                {
                    user.UserId = Guid.Parse(id);
                    user.UserName = context.User.FindFirst("emails").Value;
                    user.GivenName = context.User.FindFirst(ClaimTypes.GivenName).Value;
                    await lamaContext.AddAsync(user);
                    await lamaContext.SaveChangesAsync();
                }
            }
            await _next(context);
        }
    }
}
