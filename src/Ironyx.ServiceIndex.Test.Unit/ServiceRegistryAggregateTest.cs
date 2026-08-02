using Bogus;
using Ironyx.ServiceIndex.Domain;
using Ironyx.ServiceIndex.Domain.Models;
using Ironyx.ServiceIndex.Test.Unit.Fakers;
using Ironyx.Testing;

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
            var types = new CanonicalTypeFaker().GenerateBetween(1, 5);

            // Act
            sut.Register(name, url, types);

            // Assert
            Assert.Single(((IState<IEnumerable<Registration>>)sut).State, r => r.Id != Guid.Empty
                                                                                    && r.Name == name
                                                                                    && r.Uri == url
                                                                                    && r.CanonicalTypes == types);
        }

        [Fact(DisplayName = "[UNIT][SRA-002]: Register different service with same name")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_RegisterDifferentServiceWithSameName()
        {
            // Arrange
            var sut = CreateSUT();
            var name = new Faker().Random.String2(10);

            sut.Register(name, new Faker().Internet.Url(), new CanonicalTypeFaker().GenerateBetween(1, 5));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut.Register(name, new Faker().Internet.Url(), new CanonicalTypeFaker().GenerateBetween(1, 5)));
        }

        [Fact(DisplayName = "[UNIT][SRA-003]: Register different service with same url")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_RegisterDifferentServiceWithSameUrl()
        {
            // Arrange
            var sut = CreateSUT();
            var uri = new Faker().Internet.Url();

            sut.Register(new Faker().Random.String2(10), uri, new CanonicalTypeFaker().GenerateBetween(1, 5));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut.Register(new Faker().Random.String2(10), uri, new CanonicalTypeFaker().GenerateBetween(1, 5)));
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
            if (name is null) Assert.Throws<ArgumentNullException>(() => sut.Register(name!, new Faker().Internet.Url(), new CanonicalTypeFaker().GenerateBetween(1, 5)));
            else Assert.Throws<ArgumentException>(() => sut.Register(name, new Faker().Internet.Url(), new CanonicalTypeFaker().GenerateBetween(1, 5)));
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
            if (uri is null) Assert.Throws<ArgumentNullException>(() => sut.Register(new Faker().Random.String2(10), uri!, new CanonicalTypeFaker().GenerateBetween(1, 5)));
            else Assert.Throws<ArgumentException>(() => sut.Register(new Faker().Random.String2(10), uri, new CanonicalTypeFaker().GenerateBetween(1, 5)));
        }

        [Theory(DisplayName = "[UNIT][SRA-006]: Register Canonical Type without Type")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public void ServiceRegistryAggregate_Register_RegisterCanonicalTypeWithoutType(string? type)
        {
            // Arrange
            var sut = CreateSUT();
            var types = new CanonicalTypeFaker().WithType(type).Generate(1);

            // Act
            // Assert
            if (type is null) Assert.Throws<ArgumentNullException>(() => sut.Register(new Faker().Random.String2(10), new Faker().Internet.Url(), types));
            else Assert.Throws<ArgumentException>(() => sut.Register(new Faker().Random.String2(10), new Faker().Internet.Url(), types));
        }

        [Theory(DisplayName = "[UNIT][SRA-006]: Register Canonical Type without Version")]
        [ServiceRegistrationFeature]
        [EmptyInlineData]
        public void ServiceRegistryAggregate_Register_RegisterCanonicalTypeWithoutVersion(string? version)
        {
            // Arrange
            var sut = CreateSUT();
            var types = new CanonicalTypeFaker().WithVersion(version).Generate(1);

            // Act
            // Assert
            if (version is null) Assert.Throws<ArgumentNullException>(() => sut.Register(new Faker().Random.String2(10), new Faker().Internet.Url(), types));
            else Assert.Throws<ArgumentException>(() => sut.Register(new Faker().Random.String2(10), new Faker().Internet.Url(), types));
        }

        [Fact(DisplayName = "[UNIT][SRA-007]: Skip registration")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_SkipRegistration()
        {
            // Arrange
            var sut = CreateSUT();
            var name = new Faker().Random.String2(10);
            var url = new Faker().Internet.Url();
            var types = new CanonicalTypeFaker().GenerateBetween(1, 5);

            sut.Register(name, url, types);

            // Act
            sut.Register(name, url, types);

            // Assert
            Assert.Single(((IState<IEnumerable<Registration>>)sut).State, r => r.Id != Guid.Empty
                                                                                    && r.Name == name
                                                                                    && r.Uri == url
                                                                                    && r.CanonicalTypes == types);
        }

        [Fact(DisplayName = "[UNIT][SRA-008]: Extend registration with new version")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_ExtedRegistrationWithNewVersion()
        {
            // Arrange
            var sut = CreateSUT();
            var name = new Faker().Random.String2(10);
            var url = new Faker().Internet.Url();
            var originalTypes = new CanonicalTypeFaker().GenerateBetween(1, 5);
            var types = originalTypes.Concat(new CanonicalTypeFaker().GenerateBetween(1, 5));

            sut.Register(name, url, originalTypes);

            // Act
            sut.Register(name, url, types);

            // Assert
            Assert.Single(((IState<IEnumerable<Registration>>)sut).State, r => r.Id != Guid.Empty
                                                                                    && r.Name == name
                                                                                    && r.Uri == url
                                                                                    && r.CanonicalTypes == types);
        }

        [Fact(DisplayName = "[UNIT][SRA-009]: Registrate type and version has already been registered")]
        [ServiceRegistrationFeature]
        public void ServiceRegistryAggregate_Register_RegistrateTypeAndVersionHasAlreadyBeenRegistered()
        {
            // Arrange
            var sut = CreateSUT();
            var types = new CanonicalTypeFaker().GenerateBetween(1, 5);

            sut.Register(new Faker().Random.String2(10), new Faker().Internet.Url(), types);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut.Register(new Faker().Random.String2(10), new Faker().Internet.Url(), types));
        }
    }
}
