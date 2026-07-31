using Ironyx.ServiceIndex.Domain.Helpers;
using Ironyx.ServiceIndex.Domain.Models;

namespace Ironyx.ServiceIndex.Domain
{
    public interface IState<T>
    {
        public T State { get; }
    }

    public class ServiceRegistryAggregate : IState<IEnumerable<Registration>>
    {
        private readonly List<Registration> _registration;
        IEnumerable<Registration> IState<IEnumerable<Registration>>.State => _registration;

        public ServiceRegistryAggregate(IEnumerable<Registration> registration)
        {
            _registration = registration.ToList();
        }

        public void Register(string name, string uri)
        {
            if (_registration.Any(r => r.Name == name && r.Uri != uri)) throw Exceptions.ConflictName(name);
            if (_registration.Any(r => r.Name != name && r.Uri == uri)) throw Exceptions.ConflictUrl(name);

            _registration.Add(new Registration { Id = Guid.NewGuid(), Name = name, Uri = uri });
        }
    }
}
