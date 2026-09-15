namespace Ironyx.ServiceIndex.Infrastructure.Entities
{
    public class RegistrationEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Uri { get; set; }
        public List<CanonicalTypeEntity> CanonicalTypes { get; set; } = [];
    }
}
