using Ironyx.Kernel.Execution;
using Ironyx.ServiceIndex.Application.Observability;
using Ironyx.ServiceIndex.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Application
{
    public class GetRegistrationsQueryHandler : IQueryHandler<GetRegistrationsQuery, IEnumerable<GetRegistrationsQuery.Result>>
    {
        private readonly GetRegistrationsQueryHandlerLogEntries _logger;
        private readonly ServiceRegistryDbContext _context;

        public GetRegistrationsQueryHandler(ServiceRegistryDbContext context, ILogger<GetRegistrationsQueryHandler> logger)
        {
            _logger = new GetRegistrationsQueryHandlerLogEntries(logger);
            _context = context;
        }

        public async Task<IEnumerable<GetRegistrationsQuery.Result>> HandleAsync(GetRegistrationsQuery query, CancellationToken cancellationToken)
        {
            _logger.LogQueryingRegistrations();

            var registrations = await _context.Registrations.ToListAsync(cancellationToken);

            _logger.LogQueryedRegistrations(registrations.Count);
            return registrations.ConvertAll(r => new GetRegistrationsQuery.Result
            {
                Name = r.Name,
                Uri = r.Uri,
                Types = r.CanonicalTypes.ConvertAll(ct => new GetRegistrationsQuery.Result.CanonicalType { Type = ct.Type, Version = ct.Version })
            });
        }
    }
}
