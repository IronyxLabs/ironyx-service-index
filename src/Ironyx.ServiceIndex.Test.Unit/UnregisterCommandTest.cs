using Ironyx.ServiceIndex.Api;
using Ironyx.ServiceIndex.Test.Unit.Fakers;
using Ironyx.Testing;

namespace Ironyx.ServiceIndex.Test.Unit
{
    public class UnregisterCommandTest
    {
        [Theory(DisplayName = "[UNIT][URC-001]: Name is not defined")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public async Task UnregisterCommand_ValidateAsync_NameIsNotDefined(string? name)
        {
            // Arrange
            var sut = new UnregisterCommandValidator();

            // Act
            var result = await sut.ValidateAsync(new UnregisterCommandFaker().Name(name).Generate(), default);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
