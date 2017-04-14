using MediatR;
using EpisodeService.Data;
using EpisodeService.Data.Model;
using EpisodeService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace EpisodeService.Features.Episodes
{
    public class RemoveEpisodeCommand
    {
        public class RemoveEpisodeRequest : IRequest<RemoveEpisodeResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveEpisodeResponse { }

        public class RemoveEpisodeHandler : IAsyncRequestHandler<RemoveEpisodeRequest, RemoveEpisodeResponse>
        {
            public RemoveEpisodeHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveEpisodeResponse> Handle(RemoveEpisodeRequest request)
            {
                var episode = await _context.Episodes.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                episode.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveEpisodeResponse();
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
