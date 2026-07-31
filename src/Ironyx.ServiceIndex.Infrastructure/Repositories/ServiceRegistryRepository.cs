using Ironyx.ServiceIndex.Domain;
using Ironyx.ServiceIndex.Domain.Models;
using Ironyx.ServiceIndex.Infrastructure.Entities;
using Ironyx.ServiceIndex.Infrastructure.Observability;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Infrastructure.Repositories
{
    public class ServiceRegistryRepository : IServiceRegistryRepository
    {
        private readonly ServiceRegistryDbContext _context;
        private readonly ServiceRegistryRepositoryLogEntries _logger;

        public ServiceRegistryRepository(ServiceRegistryDbContext context, ILogger<ServiceRegistryRepository> logger)
        {
            _context = context;
            _logger = new ServiceRegistryRepositoryLogEntries(logger);
        }

        public async Task<ServiceRegistryAggregate> GetAsync(CancellationToken cancellationToken)
        {
            _logger.LogLoadingServiceRegistrations();
            var registrations = await _context.Registrations
                                                    .AsNoTracking()
                                                    .ToListAsync(cancellationToken);

            _logger.LogLoadedServiceRegistrations(registrations.Count);
            return new ServiceRegistryAggregate(registrations.ConvertAll(r => new Registration { Id = r.Id, Name = r.Name, Uri = r.Uri }));
        }

        public async Task SaveAync(IState<IEnumerable<Registration>> aggregate, CancellationToken cancellationToken)
        {
            _logger.LogSavingServiceRegistrations();

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

            foreach (var registration in aggregate.State)
            {
                var entity = await _context.Registrations.FindAsync(registration.Id, cancellationToken);
                if (entity == null)
                {
                    await _context.Registrations.AddAsync(new RegistrationEntity
                    {
                        Id = registration.Id,
                        Name = registration.Name,
                        Uri = registration.Uri
                    }, cancellationToken);
                }
                else
                {
                    entity.Name = registration.Name;
                    entity.Uri = registration.Uri;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogSavedServiceRegistrations(aggregate.State.Count());
        }
    }
}
