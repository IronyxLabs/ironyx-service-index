using FluentValidation;

namespace Ironyx.ServiceIndex
{
    [RequestVersion("v1")]
    public record UnregisterCommand : Command
    {
        public required string Name { get; init; }
    }
}

namespace Ironyx.ServiceIndex.Api
{
    public class UnregisterCommandValidator : AbstractValidator<UnregisterCommand>
    {
        public UnregisterCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
