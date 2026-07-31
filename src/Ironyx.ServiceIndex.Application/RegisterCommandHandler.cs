using Ironyx.ServiceIndex.Application.Observability;
using Ironyx.ServiceIndex.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Application
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IServiceRegistryRepository _repository;
        private readonly RegisterCommandHandlerLogEntries _logger;

        public RegisterCommandHandler(IServiceRegistryRepository repository, ILogger<RegisterCommandHandler> logger)
        {
            _repository = repository;
            _logger = new RegisterCommandHandlerLogEntries(logger);
        }

        public async Task HandleAsync(RegisterCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogRegistering(command.Name);
            var aggregate = await _repository.GetAsync(cancellationToken);
            aggregate.Register(command.Name, command.Uri);

            await _repository.SaveAync(aggregate, cancellationToken);

            _logger.LogRegistered(command.Name);
        }
    }
}
