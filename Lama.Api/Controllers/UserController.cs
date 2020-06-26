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
    public class UserController : ControllerBase
    {
        public IUserService UserService { get; set; }
        public IMapper Mapper { get; set; }
        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        [HttpGet("invitations")]
        public async Task<IEnumerable<InvitationResponse>> GetInvitations()
        {
            var invitationResponses = new List<InvitationResponse>();
            var invitations = await UserService.GetInvitations();
            foreach (var invite in invitations)
            {
                invitationResponses.Add(Mapper.Map<InvitationResponse>(invite));
            }
            return invitationResponses;
        }

        [HttpPost("invitations/{invitationId}")]
        public async Task<IActionResult> UpdateInvitation(Guid invitationId, [FromBody] InvitationStatus invitationStatus)
        {
            try
            {
                await UserService.UpdateInvitationAsync(invitationId, invitationStatus);
                return new NoContentResult();
            }
            catch(UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
        }
    }
}