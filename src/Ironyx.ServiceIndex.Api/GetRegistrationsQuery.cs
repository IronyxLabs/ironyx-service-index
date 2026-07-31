namespace Ironyx.ServiceIndex
{
    [RequestVersion("v1")]
    public record GetRegistrationsQuery : Query<IEnumerable<GetRegistrationsQuery.Result>>
    {
        public record Result
        {
            public required string Name { get; init; }
            public required string Uri { get; init; }
        }
    }
}
