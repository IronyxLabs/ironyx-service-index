using Ironyx.ServiceIndex.Api;
using Ironyx.ServiceIndex.Test.Unit.Fakers;
using Ironyx.Testing;

namespace Ironyx.ServiceIndex.Test.Unit
{
    public class RegisterCommandTest
    {
        [Theory(DisplayName = "[UNIT][RGC-001]: Name is not defined")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public async Task RegisterCommand_ValidateAsync_NameIsNotDefined(string? name)
        {
            // Arrange
            var sut = new RegisterCommandValidator();

            // Act
            var result = await sut.ValidateAsync(new RegisterCommandFaker().Name(name).Generate(), default);

            // Assert
            Assert.False(result.IsValid);
        }

        [Theory(DisplayName = "[UNIT][RGC-002]: Uri is not defined")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public async Task RegisterCommand_ValidateAsync_UriIsNotDefined(string? uri)
        {
            // Arrange
            var sut = new RegisterCommandValidator();

            // Act
            var result = await sut.ValidateAsync(new RegisterCommandFaker().Uri(uri).Generate(), default);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "[UNIT][RGC-003]: Uri is invalid")]
        [ServiceRegistrationFeature]
        public async Task RegisterCommand_ValidateAsync_UriIsInvalid()
        {
            // Arrange
            var sut = new RegisterCommandValidator();

            // Act
            var result = await sut.ValidateAsync(new RegisterCommandFaker().InvalidUri().Generate(), default);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
