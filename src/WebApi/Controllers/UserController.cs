using Core.Application.Commands;
using Core.Application.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }
    }
}
