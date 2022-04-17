using System;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Domain.SecurityHeader;
using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddRequestBodyShould
{
    private static readonly XNamespace _ns = "http://namespace.com";
    private static readonly Fixture _fixture = new();

    [Fact]
    public void AddRequestBodyWithoutHeader_WhenSecurityHeaderIsNull()
    {
        // Arrange
        XElement content = new(_ns + "Content");
        string expectation = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><Content xmlns=""http://namespace.com"" /></s:Body></s:Envelope>";
        XDocument document = new();

        // Act
        string result = document.AddRequestBody(content).ToString(SaveOptions.DisableFormatting);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void AddRequestBodyWithHeader_WhenSecurityHeaderHasValue()
    {
        // Arrange
        VaultTicket ticket = new(Guid.NewGuid());
        VaultUserId userId = new(1);
        VaultSecurityHeader securityHeader = new(ticket, userId);
        XElement content = new(_ns + "Content");
        string expectation = $@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Header><SecurityHeader xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://AutodeskDM/Services""><Ticket>{ticket}</Ticket><UserId>{userId}</UserId></SecurityHeader></s:Header><s:Body xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><Content xmlns=""http://namespace.com"" /></s:Body></s:Envelope>";
        XDocument document = new();

        // Act
        string result = document.AddRequestBody(content, securityHeader).ToString(SaveOptions.DisableFormatting);

        // Assert
        result.Should().Be(expectation);
    }
}
