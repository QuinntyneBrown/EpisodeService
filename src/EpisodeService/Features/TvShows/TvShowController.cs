using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EpisodeService.Features.Core;
using static EpisodeService.Features.TvShows.AddOrUpdateTvShowCommand;
using static EpisodeService.Features.TvShows.GetTvShowsQuery;
using static EpisodeService.Features.TvShows.GetTvShowByIdQuery;
using static EpisodeService.Features.TvShows.RemoveTvShowCommand;

namespace EpisodeService.Features.TvShows
{
    [Authorize]
    [RoutePrefix("api/tvShow")]
    public class TvShowController : ApiController
    {
        public TvShowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateTvShowResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateTvShowRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateTvShowResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateTvShowRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTvShowsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTvShowsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetTvShowByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetTvShowByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveTvShowResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveTvShowRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
