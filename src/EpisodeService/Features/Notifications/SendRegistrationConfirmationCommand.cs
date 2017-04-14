using EpisodeService.Data;
using EpisodeService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;

namespace EpisodeService.Features.Notifications
{
    public class SendRegistrationConfirmationCommand
    {
        public class SendRegistrationConfirmationRequest : IRequest<SendRegistrationConfirmationResponse>
        {
            public Guid TenantUniqueId { get; set; }
        }

        public class SendRegistrationConfirmationResponse { }

        public class SendRegistrationConfirmationHandler : IAsyncRequestHandler<SendRegistrationConfirmationRequest, SendRegistrationConfirmationResponse>
        {
            public SendRegistrationConfirmationHandler(EpisodeServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<SendRegistrationConfirmationResponse> Handle(SendRegistrationConfirmationRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly EpisodeServiceContext _context;
            private readonly ICache _cache;
        }
    }
}