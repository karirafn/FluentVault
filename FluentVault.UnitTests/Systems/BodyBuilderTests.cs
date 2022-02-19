using System;

using FluentAssertions;

using FluentVault.Common.Helpers;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class BodyBuilderTests
{
    [Fact]
    public void GetRequestBody_ShouldContainElements_WhenInputIsValid()
    {
        // Arrange
        Guid ticket = Guid.NewGuid();
        long userId = 1234L;
        string innerBody = "<someElement>someValue</someElement>";
        string expected = $@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
<s:Header>
<SecurityHeader xmlns=""http://AutodeskDM/Services"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
<Ticket>{ticket}</Ticket>
<UserId>{userId}</UserId>
</SecurityHeader>
</s:Header>
<s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
{innerBody}
</s:Body>
</s:Envelope>";

        // Act
        string? result = BodyBuilder.GetRequestBody(innerBody, ticket, userId);

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be(expected);
    }

    [Fact]
    public void GetRequestBody_ShouldNotContaineHeader_WhenInputHasNoTicketOrUserId()
    {
        // Arrange
        string innerBody = "<someElement>someValue</someElement>";
        string expected = $@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
<s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
{innerBody}
</s:Body>
</s:Envelope>";

        // Act
        string? result = BodyBuilder.GetRequestBody(innerBody);

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be(expected);
    }
}
