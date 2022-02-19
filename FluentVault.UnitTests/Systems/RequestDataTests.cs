
using System;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class RequestDataTests
{
    [Fact]
    public void SoapAction_ShouldReturnValidResult_WhenInputIsValid()
    {
        // Arrange
        string expectation = @"""http://AutodeskDM/Services/LifeCycle/1/7/2020/LifeCycleService/GetAllLifeCycleDefinitions""";

        // Act
        RequestData result = RequestData.GetAllLifeCycleDefinitions;

        // Assert
        result.SoapAction.Should().Be(expectation);
    }

    [Fact]
    public void Namespace_ShouldReturnValidResult_WhenInputIsValid()
    {
        // Arrange
        string expectation = @"http://AutodeskDM/Services/LifeCycle/1/7/2020/";

        // Act
        RequestData result = RequestData.GetAllLifeCycleDefinitions;

        // Assert
        result.Namespace.Should().Be(expectation);
    }

    [Fact]
    public void GetRequestUri_ShouldReturnValidResult_WhenInputIsValid()
    {
        // Arrange
        string expectation = @"http://server/AutodeskDM/Services/v26/LifeCycleService.svc?op=GetAllLifeCycleDefinitions&currentCommand=Connectivity.Explorer.Admin.AdminToolsCommand";
        string server = "server";
        RequestData request = RequestData.GetAllLifeCycleDefinitions;

        // Act
        Uri result = request.GetUri(server);

        // Assert
        result.Should().Be(expectation);
    }
}
