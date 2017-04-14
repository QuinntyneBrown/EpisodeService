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
    public class GetTvShowsQuery
    {
        public class GetTvShowsRequest : IRequest<GetTvShowsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetTvShowsResponse
        {
            public ICollection<TvShowApiModel> TvShows { get; set; } = new HashSet<TvShowApiModel>();
        }

        public class GetTvShowsHandler : IAsyncRequestHandler<GetTvShowsRequest, GetTvShowsResponse>
        {
            public GetTvShowsHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTvShowsResponse> Handle(GetTvShowsRequest request)
            {
                var tvShows = await _context.TvShows
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetTvShowsResponse()
                {
                    TvShows = tvShows.Select(x => TvShowApiModel.FromTvShow(x)).ToList()
                };
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
