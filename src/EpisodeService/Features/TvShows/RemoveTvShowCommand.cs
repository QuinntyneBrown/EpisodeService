using MediatR;
using EpisodeService.Data;
using EpisodeService.Data.Model;
using EpisodeService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace EpisodeService.Features.TvShows
{
    public class RemoveTvShowCommand
    {
        public class RemoveTvShowRequest : IRequest<RemoveTvShowResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveTvShowResponse { }

        public class RemoveTvShowHandler : IAsyncRequestHandler<RemoveTvShowRequest, RemoveTvShowResponse>
        {
            public RemoveTvShowHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveTvShowResponse> Handle(RemoveTvShowRequest request)
            {
                var tvShow = await _context.TvShows.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                tvShow.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveTvShowResponse();
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
