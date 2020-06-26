using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lama.Api.Data.Models;
using Lama.Api.Data.Services.Interfaces;
using Lama.Api.Exceptions;
using Lama.Api.Responses;
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
        private readonly IMapper _mapper;

        public ClubsController(IClubsService clubsService, IMapper mapper)
        {
            _clubsService = clubsService;
            _mapper = mapper;
        }

        // GET: api/Club
        [HttpGet]
        public async Task<IEnumerable<ClubResponse>> Get()
        {
            var clubs = await _clubsService.GetLoggedInUsersClubs();
            var response = new List<ClubResponse>();
            foreach(var c in clubs)
            {
                response.Add(_mapper.Map<ClubResponse>(c));
            }
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Club club)
        {
            var clubRes = await _clubsService.Add(club.ClubName);

            return new CreatedResult(HttpContext.GetEndpoint().DisplayName, _mapper.Map<ClubResponse>(clubRes));
        }

        // GET: api/Club/5
        [HttpGet("{name}", Name = "Get")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                name = name.Replace('-', ' ');
                var club = await _clubsService.GetClubByName(name);
                var clubResponse = _mapper.Map<ClubDetailsResponse>(club);
                return new JsonResult(clubResponse);
            }
            catch (UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }


        [HttpPost("{id}/invite")]
        public async Task<IActionResult> Invite(Guid id, [FromBody] string userName)
        {
            try
            {
                await _clubsService.AddInvitation(id, userName);
                return new NoContentResult();
            }
            catch (UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
            catch (KeyNotFoundException ex )
            {
                return new NotFoundObjectResult(ex.Message);
            }catch(InvalidOperationException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
