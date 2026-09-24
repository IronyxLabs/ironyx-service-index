using Ironyx.Kernel;

namespace Ironyx.ServiceIndex.Domain.Helpers
{
    public static class Exceptions
    {
        public static class ServiceRegistration
        {
            private const string SUBJECT = "Service Registration";
            private const string RESOURCE_TYPE = "Service Registration";

            public static BusinessRuleException ConflictName(string name) => new("REG_ERR_001", SUBJECT, $"Service with {name} name has already been registered") { ResourceName = "Name", ResourceType = RESOURCE_TYPE };
            public static BusinessRuleException ConflictUrl(string url) => new("REG_ERR_002", SUBJECT, $"Service with {url} url has already been registered") { ResourceName = "Url", ResourceType = RESOURCE_TYPE };
            public static BusinessRuleException ConflictType(string type, string version) => new("REG_ERR_003", SUBJECT, $"Canonical type {type}, version {version} has already been registered") { ResourceName = "Type", ResourceType = RESOURCE_TYPE };
        }
    }
}
