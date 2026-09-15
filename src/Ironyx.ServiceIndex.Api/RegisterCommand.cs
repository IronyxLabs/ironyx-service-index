using FluentValidation;

namespace Ironyx.ServiceIndex
{
    [RequestVersion("v1")]
    public record RegisterCommand : Command
    {
        public required string Name { get; init; }
        public required string Uri { get; init; }
        public IEnumerable<CanonicalType> Types { get; init; } = [];

        public record CanonicalType
        {
            public required string Type { get; init; }
            public required string Version { get; init; }
        }
    }
}

namespace Ironyx.ServiceIndex.Api
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Uri).NotEmpty();
            RuleFor(c => c.Uri).Must(ValidateUrl).WithMessage("URL must be a valid absolute URL");
        }

        private bool ValidateUrl(string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uri)
                && uri.Scheme is "http" or "https";
        }
    }
}