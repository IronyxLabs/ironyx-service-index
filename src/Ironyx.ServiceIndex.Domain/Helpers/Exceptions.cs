namespace Ironyx.ServiceIndex.Domain.Helpers
{
    public static class Exceptions
    {
        public static InvalidOperationException ConflictName(string name) => new($"Service with {name} name has already been registered");
        public static InvalidOperationException ConflictUrl(string url) => new($"Service with {url} url has already been registered");
        public static InvalidOperationException ConflictType(string type, string version) => new($"Canonical type {type}, version {version} has already been registered");
    }
}
