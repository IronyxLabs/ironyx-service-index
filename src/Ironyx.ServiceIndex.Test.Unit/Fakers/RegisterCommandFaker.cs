using AutoBogus;

namespace Ironyx.ServiceIndex.Test.Unit.Fakers
{
    public class RegisterCommandFaker : AutoFaker<RegisterCommand>
    {
        public RegisterCommandFaker()
        {
            RuleFor(c => c.Uri, f => f.Internet.Url());
        }

        public RegisterCommandFaker Name(string? name)
        {
            RuleFor(c => c.Name, name);

            return this;
        }

        public RegisterCommandFaker Uri(string? uri)
        {
            RuleFor(c => c.Uri, uri);

            return this;
        }

        public RegisterCommandFaker InvalidUri()
        {
            RuleFor(c => c.Uri, f => f.Random.String2(10));

            return this;
        }
    }
}
