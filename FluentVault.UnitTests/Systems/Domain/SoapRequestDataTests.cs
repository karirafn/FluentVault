
using FluentAssertions;

using FluentValidation;

using FluentVault.Domain.SOAP;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain;

public class SoapRequestDataTests
{
    private const string Name = "GetSomething";
    private const string Version = "v26";
    private const string Service = "SomeService";
    private const string Command = "This.Is.A.Command";
    private const string Namespace = "Services/Something/1/1/2000";

    private static readonly SoapRequestData _data = new(Name, Version, Service, Command, Namespace);

    [Theory]
    [InlineData(Name)]
    [InlineData("FindEverything")]
    [InlineData("UpdateStuff")]
    [InlineData("SignAutograph")]
    public void ConstructorShould_Construct_WhenNameIsValid(string name)
        => _ = new SoapRequestData(name, Version, Service, Command, Namespace);

    [Theory]
    [InlineData("")]
    [InlineData("getSomething")]
    [InlineData("Getsometing")]
    [InlineData("Get")]
    [InlineData("WatchSomething")]
    public void ConstructorShould_ThrowException_WhenNameIsInvalid(string name)
        => Assert.Throws<ValidationException>(() => new SoapRequestData(name, Version, Service, Command, Namespace));

    [Theory]
    [InlineData(Version)]
    [InlineData("v26_2")]
    [InlineData("Filestore/v26")]
    [InlineData("Filestore/v26_2")]
    public void ConstructorShould_Construct_WhenVersionIsValid(string version)
        => _ = new SoapRequestData(Name, version, Service, Command, Namespace);

    [Theory]
    [InlineData("")]
    [InlineData("v26_33")]
    [InlineData("26_2")]
    [InlineData("vv26_2")]
    [InlineData("26")]
    [InlineData("Smilestore/v26")]
    [InlineData("Services/v26")]
    public void ConstructorShould_ThrowException_WhenVersionIsInvalid(string version)
        => Assert.Throws<ValidationException>(() => new SoapRequestData(Name, version, Service, Command, Namespace));

    [Theory]
    [InlineData(Service)]
    [InlineData("SomeServiceExtensions")]
    public void ConstructorShould_Construct_WhenServiceIsValid(string service)
        => _ = new SoapRequestData(Name, Version, service, Command, Namespace);

    [Theory]
    [InlineData("")]
    [InlineData("Service")]
    [InlineData("ServiceExtensions")]
    [InlineData("SomeServiceSomething")]
    public void ConstructorShould_ThrowException_WhenServiceIsInvalid(string service)
        => Assert.Throws<ValidationException>(() => new SoapRequestData(Name, Version, service, Command, Namespace));

    [Theory]
    [InlineData(Command)]
    [InlineData("")]
    public void ConstructorShould_Construct_WhenCommandIsValid(string command)
        => _ = new SoapRequestData(Name, Version, Service, command, Namespace);

    [Theory]
    [InlineData("This")]
    [InlineData("This.Is")]
    [InlineData("This.Is.A")]
    [InlineData("Command")]
    public void ConstructorShould_ThrowException_WhenCommandIsInvalid(string command)
        => Assert.Throws<ValidationException>(() => new SoapRequestData(Name, Version, Service, command, Namespace));

    [Theory]
    [InlineData(Namespace)]
    [InlineData("Filestore/Auth/1/7/2020")]
    public void ConstructorShould_Construct_WhenNamespaceIsValid(string @namespace)
        => _ = new SoapRequestData(Name, Version, Service, Command, @namespace);

    [Theory]
    [InlineData("")]
    [InlineData("Services/Category/32/7/2020")]
    [InlineData("Services/Category/1/13/2020")]
    [InlineData("Services/Category/1/17/500")]
    [InlineData("Something/Category/1/7/2020")]
    [InlineData("Services/Something/1/17/500/")]
    public void ConstructorShould_ThrowException_WhenNamespaceIsInvalid(string @namespace)
        => Assert.Throws<ValidationException>(() => new SoapRequestData(Name, Version, Service, Command, @namespace));

    [Fact]
    public void NamespaceShould_ReturnNamespace_WhenDataIsValid()
    {
        // Arrange
        string expectation = @"http://AutodeskDM/Services/Something/1/1/2000";

        // Act
        string @namespace = _data.Namespace;

        // Assert
        @namespace.Should().Be(expectation);
    }

    [Fact]
    public void UriShould_ReturnUriWithCommand_WhenCommandIsNotEmpty()
    {
        // Arrange
        string expectation = @"AutodeskDM/Services/v26/SomeService.svc?op=GetSomething&currentCommand=This.Is.A.Command";

        // Act
        string uri = _data.Uri;

        // Assert
        uri.Should().Be(expectation);
    }

    [Fact]
    public void UriShould_ReturnUriWithoutCommand_WhenCommandIsEmpty()
    {
        // Arrange
        SoapRequestData data = new(Name, Version, Service, string.Empty, Namespace);
        string expectation = @"AutodeskDM/Services/v26/SomeService.svc";

        // Act
        string uri = data.Uri;

        // Assert
        uri.Should().Be(expectation);
    }

    [Fact]
    public void SoapActionShould_ReturnSoapAction_WhenDataIsValid()
    {
        // Arrange
        string expectation = @"http://AutodeskDM/Services/Something/1/1/2000/SomeService/GetSomething";

        // Act
        string soapAction = _data.SoapAction;

        // Assert
        soapAction.Should().Be(expectation);
    }
}
