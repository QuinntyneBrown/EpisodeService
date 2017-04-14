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
    public class AddOrUpdateEpisodeCommand
    {
        public class AddOrUpdateEpisodeRequest : IRequest<AddOrUpdateEpisodeResponse>
        {
            public EpisodeApiModel Episode { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateEpisodeResponse { }

        public class AddOrUpdateEpisodeHandler : IAsyncRequestHandler<AddOrUpdateEpisodeRequest, AddOrUpdateEpisodeResponse>
        {
            public AddOrUpdateEpisodeHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateEpisodeResponse> Handle(AddOrUpdateEpisodeRequest request)
            {
                var entity = await _context.Episodes
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Episode.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Episodes.Add(entity = new Episode() { TenantId = tenant.Id });
                }

                entity.Name = request.Episode.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateEpisodeResponse();
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
