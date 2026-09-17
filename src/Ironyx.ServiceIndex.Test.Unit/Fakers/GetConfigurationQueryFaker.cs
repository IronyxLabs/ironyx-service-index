using AutoBogus;

namespace Ironyx.ServiceIndex.Test.Unit.Fakers
{
    public class GetConfigurationQueryFaker : AutoFaker<GetConfigurationQuery>
    {
        public GetConfigurationQueryFaker Name(string? name)
        {
            RuleFor(q => q.Type, name);

            return this;
        }

        public GetConfigurationQueryFaker Version(string? version)
        {
            RuleFor(q => q.Version, version);

            return this;
        }
    }
}
