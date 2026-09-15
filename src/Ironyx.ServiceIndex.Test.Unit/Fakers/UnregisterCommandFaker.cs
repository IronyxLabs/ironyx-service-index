using AutoBogus;

namespace Ironyx.ServiceIndex.Test.Unit.Fakers
{
    public class UnregisterCommandFaker : AutoFaker<UnregisterCommand>
    {
        public UnregisterCommandFaker Name(string? name)
        {
            RuleFor(c => c.Name, name);

            return this;
        }
    }
}
