using MediatR;
using EpisodeService.Data;
using EpisodeService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace EpisodeService.Features.Episodes
{
    public class GetEpisodesQuery
    {
        public class GetEpisodesRequest : IRequest<GetEpisodesResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetEpisodesResponse
        {
            public ICollection<EpisodeApiModel> Episodes { get; set; } = new HashSet<EpisodeApiModel>();
        }

        public class GetEpisodesHandler : IAsyncRequestHandler<GetEpisodesRequest, GetEpisodesResponse>
        {
            public GetEpisodesHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetEpisodesResponse> Handle(GetEpisodesRequest request)
            {
                var episodes = await _context.Episodes
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetEpisodesResponse()
                {
                    Episodes = episodes.Select(x => EpisodeApiModel.FromEpisode(x)).ToList()
                };
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
