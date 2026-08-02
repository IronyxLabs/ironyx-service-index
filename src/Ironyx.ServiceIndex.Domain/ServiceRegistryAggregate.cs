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

        public void Register(string name, string uri, IEnumerable<CanonicalType> types)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(uri);

            types.Validate();

            if (_registration.Any(r => r.Name == name && r.Uri != uri)) throw Exceptions.ConflictName(name);
            if (_registration.Any(r => r.Name != name && r.Uri == uri)) throw Exceptions.ConflictUrl(name);

            var registration = _registration.IsRegistered(name, uri);

            if (registration is null) _registration.Add(new Registration { Id = Guid.NewGuid(), Name = name, Uri = uri, CanonicalTypes = types });
            else registration.CanonicalTypes = types;
        }
    }

    file static class ServiceRegistryAggregateExtensions
    {
        public static Registration? IsRegistered(this IEnumerable<Registration> registrations, string name, string uri)
        {
            return registrations.SingleOrDefault(r => r.Name == name && r.Uri == uri);
        }

        public static void Validate(this IEnumerable<CanonicalType> types)
        {
            foreach (var type in types)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(type.Type);
                ArgumentException.ThrowIfNullOrWhiteSpace(type.Version);
            }
        }
    }
}
