using EpisodeService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static EpisodeService.Features.Users.AddOrUpdateUserCommand;
using static EpisodeService.Features.Users.GetUsersQuery;
using static EpisodeService.Features.Users.GetUserByIdQuery;
using static EpisodeService.Features.Users.RemoveUserCommand;
using static EpisodeService.Features.Users.GetUserByUsernameQuery;

namespace EpisodeService.Features.Users
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateUserResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateUserRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateUserResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateUserRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetUsersResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetUsersQuery.GetUsersRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetUserByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetUserByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveUserResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveUserRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("current")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetUserByUsernameResponse))]
        public async Task<IHttpActionResult> Current()
        {            
            if (!User.Identity.IsAuthenticated)
                return Ok();
            var request = new GetUserByUsernameRequest();
            request.Username = User.Identity.Name;
            var user = await _userManager.GetUserAsync(User);
            request.TenantId = user.TenantId;
            
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
