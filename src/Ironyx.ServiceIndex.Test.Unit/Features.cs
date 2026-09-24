using Ironyx.Testing;

namespace Ironyx.ServiceIndex.Test.Unit
{
    public class ServiceRegistrationFeatureAttribute : FeatureAttribute
    {
        public ServiceRegistrationFeatureAttribute() : base("SRV", "Service Registration")
        {
        }
    }

    public class GetConfigurationFeatureAttribute : FeatureAttribute
    {
        public GetConfigurationFeatureAttribute() : base("GCF", "Get Configuration")
        {
        }
    }
}
