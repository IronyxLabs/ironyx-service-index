using Ironyx.Kernel;

namespace Ironyx.ServiceIndex.Domain.Helpers
{
    public static class Exceptions
    {
        public static BusinessRuleException ConflictName(string name) => new("REG_ERR_001", "Service Registration", $"Service with {name} name has already been registered") { ResourceName = "Name", ResourceType = "Service Registration" };
        public static BusinessRuleException ConflictUrl(string url) => new("REG_ERR_002", "Service Registration", $"Service with {url} url has already been registered") { ResourceName = "Url", ResourceType = "Service Registration" };
        public static BusinessRuleException ConflictType(string type, string version) => new("REG_ERR_003", "Service Registration", $"Canonical type {type}, version {version} has already been registered") { ResourceName = "Type", ResourceType = "Service Registration" };
    }
}
