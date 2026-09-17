using Ironyx.Kernel.Execution;
using Ironyx.ServiceIndex.Application.Observability;
using Ironyx.ServiceIndex.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Application
{
    public class GetConfigurationQueryHandler : IQueryHandler<GetConfigurationQuery, GetConfigurationQuery.Result>
    {
        private readonly ServiceRegistryDbContext _context;
        private readonly GetConfigruationQueryHandlerLogEntries _logger;

        public GetConfigurationQueryHandler(ServiceRegistryDbContext context, ILogger<GetConfigurationQueryHandler> logger)
        {
            _context = context;
            _logger = new GetConfigruationQueryHandlerLogEntries(logger);
        }

        public async Task<GetConfigurationQuery.Result> HandleAsync(GetConfigurationQuery query, CancellationToken cancellationToken)
        {
            _logger.LogResolvingConfiguration(query.Type, query.Version);

            var service = await _context.Registrations.SingleOrDefaultAsync(r => r.CanonicalTypes.Any(ct => ct.Type == query.Type && ct.Version == query.Version), cancellationToken);

            if (service is null)
            {
                throw new BusinessRuleException("GET_CONFIG_001", "Service Configuration", "Service was not found for type Product.Create and version v1")
                {
                    ResourceType = "Service",
                    ResourceName = "Configuration"
                };
            }

            _logger.LogResolvedConfiguration(query.Type, query.Version);
            return new GetConfigurationQuery.Result { Uri = new Uri(service.Uri) };
        }
    }
}
