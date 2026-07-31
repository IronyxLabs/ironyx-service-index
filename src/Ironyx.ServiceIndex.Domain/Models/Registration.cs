namespace Ironyx.ServiceIndex.Domain.Models
{
    public class Registration
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Uri { get; init; }
        public required IEnumerable<CanonicalType> CanonicalTypes { get; init; }

    }
}
