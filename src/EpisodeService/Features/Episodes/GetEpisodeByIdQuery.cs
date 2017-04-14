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
    public class GetEpisodeByIdQuery
    {
        public class GetEpisodeByIdRequest : IRequest<GetEpisodeByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetEpisodeByIdResponse
        {
            public EpisodeApiModel Episode { get; set; } 
        }

        public class GetEpisodeByIdHandler : IAsyncRequestHandler<GetEpisodeByIdRequest, GetEpisodeByIdResponse>
        {
            public GetEpisodeByIdHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetEpisodeByIdResponse> Handle(GetEpisodeByIdRequest request)
            {                
                return new GetEpisodeByIdResponse()
                {
                    Episode = EpisodeApiModel.FromEpisode(await _context.Episodes
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
