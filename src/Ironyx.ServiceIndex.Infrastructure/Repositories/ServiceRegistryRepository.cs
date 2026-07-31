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
            return new ServiceRegistryAggregate(registrations.ConvertAll(r => new Registration
            {
                Id = r.Id,
                Name = r.Name,
                Uri = r.Uri,
                CanonicalTypes = r.CanonicalTypes.ConvertAll(ct => ct.ToValueObject())
            }));
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
                        Uri = registration.Uri,
                        CanonicalTypes = [.. registration.CanonicalTypes.ConvertAll()],
                    }, cancellationToken);
                }
                else
                {
                    entity.Name = registration.Name;
                    entity.Uri = registration.Uri;
                    entity.CanonicalTypes = [.. registration.CanonicalTypes.ConvertAll()];
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogSavedServiceRegistrations(aggregate.State.Count());
        }
    }

    file static class ServiceRegistryRepositoryExtensions
    {
        public static IEnumerable<CanonicalTypeEntity> ConvertAll(this IEnumerable<CanonicalType> types)
        {
            foreach (var type in types)
            {
                yield return type.ToEntity();
            }
        }

        public static CanonicalTypeEntity ToEntity(this CanonicalType type)
        {
            return new CanonicalTypeEntity { Type = type.Type, Version = type.Version };
        }

        public static CanonicalType ToValueObject(this CanonicalTypeEntity type)
        {
            return new CanonicalType { Type = type.Type, Version = type.Version };
        }
    }
}
