using Ironyx.ServiceIndex.Api;
using Ironyx.ServiceIndex.Test.Unit.Fakers;
using Ironyx.Testing;

namespace Ironyx.ServiceIndex.Test.Unit
{
    public class GetConfigurationQueryTest
    {
        [Theory(DisplayName = "[UNIT][GCQ-001]: Name is not defined")]
        [GetConfigurationFeature]
        [EmptyInlineData]
        public async Task GetConfigurationQuery_ValidateAsync_NameIsNotDefined(string? name)
        {
            // Arrange
            var sut = new GetConfigurationQueryValidator();

            // Act
            var result = await sut.ValidateAsync(new GetConfigurationQueryFaker().Name(name).Generate(), default);

            // Assert
            Assert.False(result.IsValid);
        }

        [Theory(DisplayName = "[UNIT][GCQ-002]: Version is not defined")]
        [GetConfigurationFeature]
        [EmptyInlineData]
        public async Task GetConfigurationQuery_ValidateAsync_VersionIsNotDefined(string? version)
        {
            // Arrange
            var sut = new GetConfigurationQueryValidator();

            // Act
            var result = await sut.ValidateAsync(new GetConfigurationQueryFaker().Version(version).Generate(), default);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
