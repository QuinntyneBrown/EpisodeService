using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EpisodeService.Features.Core;
using static EpisodeService.Features.Episodes.AddOrUpdateEpisodeCommand;
using static EpisodeService.Features.Episodes.GetEpisodesQuery;
using static EpisodeService.Features.Episodes.GetEpisodeByIdQuery;
using static EpisodeService.Features.Episodes.RemoveEpisodeCommand;

namespace EpisodeService.Features.Episodes
{
    [Authorize]
    [RoutePrefix("api/episode")]
    public class EpisodeController : ApiController
    {
        public EpisodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateEpisodeResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateEpisodeRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateEpisodeResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateEpisodeRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetEpisodesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetEpisodesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetEpisodeByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetEpisodeByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveEpisodeResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveEpisodeRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
