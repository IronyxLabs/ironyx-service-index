using Bogus;
using Ironyx.ServiceIndex.Domain;
using Ironyx.ServiceIndex.Domain.Models;
using Ironyx.ServiceIndex.Test.Unit.Attributes;

namespace Ironyx.ServiceIndex.Test.Unit
{
    public class ServiceRegistryAggregateTest
    {
        private ServiceRegistryAggregate CreateSUT()
        {
            return new ServiceRegistryAggregate([]);
        }

        [Fact(DisplayName = "[UNIT][SRA-001]: Register service")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_RegisterService()
        {
            // Arrange
            var sut = CreateSUT();
            var name = new Faker().Random.String2(10);
            var url = new Faker().Internet.Url();

            // Act
            sut.Register(name, url);

            // Assert
            Assert.Single(((IState<IEnumerable<Registration>>)sut).State, r => r.Id != Guid.Empty
                                                                                    && r.Name == name
                                                                                    && r.Uri == url);
        }

        [Fact(DisplayName = "[UNIT][SRA-002]: Register different service with same name")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_RegisterDifferentServiceWithSameName()
        {
            // Arrange
            var sut = CreateSUT();
            var name = new Faker().Random.String2(10);

            sut.Register(name, new Faker().Internet.Url());

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut.Register(name, new Faker().Internet.Url()));
        }

        [Fact(DisplayName = "[UNIT][SRA-003]: Register different service with same url")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_RegisterDifferentServiceWithSameUrl()
        {
            // Arrange
            var sut = CreateSUT();
            var uri = new Faker().Internet.Url();

            sut.Register(new Faker().Random.String2(10), uri);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut.Register(new Faker().Random.String2(10), uri));
        }

        [Theory(DisplayName = "[UNIT][SRA-004]: Name is empty")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public void ServiceRegistryAggregate_Register_NameIsEmpty(string? name)
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            // Assert
            if (name is null) Assert.Throws<ArgumentNullException>(() => sut.Register(name!, new Faker().Internet.Url()));
            else Assert.Throws<ArgumentException>(() => sut.Register(name, new Faker().Internet.Url()));
        }

        [Theory(DisplayName = "[UNIT][SRA-005]: Uri is empty")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public void ServiceRegistryAggregate_Register_UriIsEmpty(string? uri)
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            // Assert
            if (uri is null) Assert.Throws<ArgumentNullException>(() => sut.Register(new Faker().Random.String2(10), uri!));
            else Assert.Throws<ArgumentException>(() => sut.Register(new Faker().Random.String2(10), uri));
        }
    }
}
