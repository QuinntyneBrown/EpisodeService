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
    public class AddOrUpdateTvShowCommand
    {
        public class AddOrUpdateTvShowRequest : IRequest<AddOrUpdateTvShowResponse>
        {
            public TvShowApiModel TvShow { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateTvShowResponse { }

        public class AddOrUpdateTvShowHandler : IAsyncRequestHandler<AddOrUpdateTvShowRequest, AddOrUpdateTvShowResponse>
        {
            public AddOrUpdateTvShowHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateTvShowResponse> Handle(AddOrUpdateTvShowRequest request)
            {
                var entity = await _context.TvShows
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.TvShow.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.TvShows.Add(entity = new TvShow() { TenantId = tenant.Id });
                }

                entity.Name = request.TvShow.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateTvShowResponse();
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
