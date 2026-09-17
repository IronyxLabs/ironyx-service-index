using FluentValidation;

namespace Ironyx.ServiceIndex
{
    [RequestVersion("v1")]
    public record GetConfigurationQuery : Query<GetConfigurationQuery.Result>
    {
        public required string Type { get; init; }
        public required string Version { get; init; }

        public record Result
        {
            public required Uri Uri { get; init; }
        }
    }
}

namespace Ironyx.ServiceIndex.Api
{
    public class GetConfigurationQueryValidator : AbstractValidator<GetConfigurationQuery>
    {
        public GetConfigurationQueryValidator()
        {
            RuleFor(q => q.Type).NotEmpty();
            RuleFor(q => q.Version).NotEmpty();
        }
    }
}
