namespace Ironyx.ServiceIndex
{
    [RequestVersion("v1")]
    public record GetRegistrationsQuery : Query<IEnumerable<GetRegistrationsQuery.Result>>
    {
        public record Result
        {
            public required string Name { get; init; }
            public required string Uri { get; init; }
            public required IEnumerable<CanonicalType> Types { get; init; }

            public record CanonicalType
            {
                public required string Type { get; init; }
                public required string Version { get; init; }
            }
        }
    }
}
