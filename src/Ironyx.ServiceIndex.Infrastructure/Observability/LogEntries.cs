using Ironyx.ServiceIndex.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Infrastructure.Observability
{
    internal partial class ServiceRegistryRepositoryLogEntries
    {
        private readonly ILogger<ServiceRegistryRepository> _logger;

        public ServiceRegistryRepositoryLogEntries(ILogger<ServiceRegistryRepository> logger)
        {
            _logger = logger;
        }

        [LoggerMessage(LogLevel.Debug, Message = "Loading service registrations")]
        public partial void LogLoadingServiceRegistrations();

        [LoggerMessage(LogLevel.Debug, Message = "Loaded {Count} service registrations")]
        public partial void LogLoadedServiceRegistrations(int count);

        [LoggerMessage(LogLevel.Debug, Message = "Saving service registrations")]
        public partial void LogSavingServiceRegistrations();

        [LoggerMessage(LogLevel.Debug, Message = "{Count} service registration(s) has been saved")]
        public partial void LogSavedServiceRegistrations(int count);
    }
}
