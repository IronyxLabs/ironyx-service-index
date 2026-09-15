using Ironyx.ServiceIndex.Api;
using Ironyx.ServiceIndex.Application.Observability;
using Ironyx.ServiceIndex.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Application
{
    public class UnregisterCommandHandler : ICommandHandler<UnregisterCommand>
    {
        private readonly IServiceRegistryRepository _repository;
        private readonly UnregisterCommandHandlerLogEntries _logger;

        public UnregisterCommandHandler(IServiceRegistryRepository repository, ILogger<UnregisterCommandHandler> logger)
        {
            _repository = repository;
            _logger = new UnregisterCommandHandlerLogEntries(logger);
        }

        public async Task HandleAsync(UnregisterCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogUnregistering(command.Name);

            var aggregate = await _repository.GetAsync(cancellationToken);
            aggregate.Unregister(command.Name);

            _logger.LogUnregistered(command.Name);
        }
    }
}
