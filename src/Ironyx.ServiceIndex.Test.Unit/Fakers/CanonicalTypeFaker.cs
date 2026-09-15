using AutoBogus;
using Ironyx.ServiceIndex.Domain.Models;

namespace Ironyx.ServiceIndex.Test.Unit.Fakers
{
    internal class CanonicalTypeFaker : AutoFaker<CanonicalType>
    {
        public CanonicalTypeFaker WithType(string? type)
        {
            RuleFor(ct => ct.Type, type);

            return this;
        }

        public CanonicalTypeFaker WithVersion(string? version)
        {
            RuleFor(ct => ct.Version, version);

            return this;
        }
    }
}
