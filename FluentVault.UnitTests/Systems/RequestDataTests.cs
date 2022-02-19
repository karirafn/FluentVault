
using System;
using System.Collections.Generic;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class RequestDataTests
{
    [Fact]
    public void Constructor_ShouldThrowException_WhenInvalidName()
        => Assert.Throws<KeyNotFoundException>(() => new RequestData("Invalid").SoapAction);

    [Theory]
    [InlineData("SignOut", @"""http://AutodeskDM/Filestore/Auth/1/8/2021/AuthService/SignOut""")]
    [InlineData("GetAllLifeCycleDefinitions", @"""http://AutodeskDM/Services/LifeCycle/1/7/2020/LifeCycleService/GetAllLifeCycleDefinitions""")]
    public void SoapAction_ShouldReturnValidResult_WhenInputIsValid(string name, string expectation)
    {
        // Arrange

        // Act
        RequestData result = new(name);

        // Assert
        result.SoapAction.Should().Be(expectation);
    }

    [Fact]
    public void Namespace_ShouldReturnValidResult_WhenInputIsValid()
    {
        // Arrange
        string name = "GetAllLifeCycleDefinitions";
        string expectation = @"http://AutodeskDM/Services/LifeCycle/1/7/2020/";

        // Act
        RequestData result = new(name);

        // Assert
        result.Namespace.Should().Be(expectation);
    }

    [Theory]
    [InlineData("SignIn", "http://server/AutodeskDM/Services/Filestore/v26/AuthService.svc")]
    [InlineData("GetAllLifeCycleDefinitions", @"http://server/AutodeskDM/Services/v26/LifeCycleService.svc?op=GetAllLifeCycleDefinitions&currentCommand=Connectivity.Explorer.Admin.AdminToolsCommand")]
    public void GetRequestUri_ShouldReturnValidResult_WhenInputIsValid(string name, string expectation)
    {
        // Arrange
        string server = "server";
        RequestData request = new(name);

        // Act
        Uri result = request.GetUri(server);

        // Assert
        result.Should().Be(expectation);
    }
}
