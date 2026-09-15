using Ironyx.ServiceIndex.Domain;
using Ironyx.ServiceIndex.Domain.Models;

namespace Ironyx.ServiceIndex.Infrastructure.Repositories
{
    public interface IServiceRegistryRepository
    {
        Task<ServiceRegistryAggregate> GetAsync(CancellationToken cancellationToken);
        Task SaveAync(IState<IEnumerable<Registration>> aggregate, CancellationToken cancellationToken);
    }
}
