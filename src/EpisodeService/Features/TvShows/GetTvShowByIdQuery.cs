using MediatR;
using EpisodeService.Data;
using EpisodeService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace EpisodeService.Features.TvShows
{
    public class GetTvShowByIdQuery
    {
        public class GetTvShowByIdRequest : IRequest<GetTvShowByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetTvShowByIdResponse
        {
            public TvShowApiModel TvShow { get; set; } 
        }

        public class GetTvShowByIdHandler : IAsyncRequestHandler<GetTvShowByIdRequest, GetTvShowByIdResponse>
        {
            public GetTvShowByIdHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTvShowByIdResponse> Handle(GetTvShowByIdRequest request)
            {                
                return new GetTvShowByIdResponse()
                {
                    TvShow = TvShowApiModel.FromTvShow(await _context.TvShows
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
