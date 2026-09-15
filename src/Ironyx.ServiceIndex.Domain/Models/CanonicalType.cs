namespace Ironyx.ServiceIndex.Domain.Models
{
    public record CanonicalType
    {
        public required string Type { get; init; }
        public required string Version { get; init; }
    }
}
