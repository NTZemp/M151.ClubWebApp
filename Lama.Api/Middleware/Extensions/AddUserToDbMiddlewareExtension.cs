using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lama.Api.Middleware.Extensions
{
    public static class AddUserToDbMiddlewareExtension
    {
        public static IApplicationBuilder UseAddUserToDbIfNotExists(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddUserToDbMiddleware>();
        }
    }
}
