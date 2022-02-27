using System;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;
using FluentVault.Domain;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddRequestBodyShould
{
    private static readonly XNamespace _ns = "http://namespace.com";

    [Fact]
    public void AddRequestBodyWithoutHeader_WhenSessionCredentialsAreDefault()
    {
        // Arrange
        VaultSessionCredentials session = new();
        XElement content = new(_ns + "Content");
        string expectation = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><Content xmlns=""http://namespace.com"" /></s:Body></s:Envelope>";
        XDocument document = new();

        // Act
        string result = document.AddRequestBody(session, content).ToString(SaveOptions.DisableFormatting);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AddRequestBodyWithHeader_WhenSessionCredentialsHaveValue()
    {
        // Arrange
        Guid ticket = Guid.NewGuid();
        long userId = 1;
        VaultSessionCredentials session = new(ticket, userId);
        XElement content = new(_ns + "Content");
        string expectation = $@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Header><SecurityHeader xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://AutodeskDM/Services""><Ticket>{ticket}</Ticket><UserId>{userId}</UserId></SecurityHeader></s:Header><s:Body xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><Content xmlns=""http://namespace.com"" /></s:Body></s:Envelope>";
        XDocument document = new();

        // Act
        string result = document.AddRequestBody(session, content).ToString(SaveOptions.DisableFormatting);

        // Assert
        result.Should().Be(expectation);
    }
}
