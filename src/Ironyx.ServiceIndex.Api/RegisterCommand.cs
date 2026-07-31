namespace Ironyx.ServiceIndex
{
    [RequestVersion("v1")]
    public record RegisterCommand : Command
    {
        public required string Name { get; init; }
        public required string Uri { get; init; }
    }
}
