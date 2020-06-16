using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lama.Api.Data.Models;
using Lama.Api.Data.Services.Interfaces;
using Lama.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lama.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IClubsService _clubsService;

        public ClubsController(IClubsService clubsService)
        {
            _clubsService = clubsService;
        }

        // GET: api/Club
        [HttpGet]
        public async Task<IEnumerable<Club>> Get()
        {
            return await _clubsService.GetLoggedInUsersClubs();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Club club)
        {
            var clubRes = await _clubsService.Add(club.ClubName);
            return new CreatedResult(HttpContext.GetEndpoint().DisplayName);
        }

        // GET: api/Club/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return new JsonResult(await _clubsService.GetClub(id));
            }
            catch (UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }

        [HttpPost("{id}/Add")]
        public async Task<IActionResult> AddMember(Guid id, [FromBody] ApiUser user)
        {
            try
            {
                return new JsonResult(await _clubsService.AddMember(id,user));
            }
            catch (UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
