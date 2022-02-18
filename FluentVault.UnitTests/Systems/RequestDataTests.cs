
using System;
using System.Collections.Generic;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class RequestDataTests
{
    [Theory]
    [InlineData("SignOut", @"""http://AutodeskDM/Filestore/Auth/1/8/2021/AuthService/SignOut""")]
    [InlineData("GetAllLifeCycleDefinitions", @"""http://AutodeskDM/Services/LifeCycle/1/7/2020/LifeCycleService/GetAllLifeCycleDefinitions""")]
    public void GetSoapAction_ShouldReturnValidResult_WhenInputIsValid(string name, string expectation)
    {
        // Arrange

        // Act
        string result = RequestData.GetSoapAction(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetSoapAction_ShouldThrowException_WhenInputIsInvalid()
        => Assert.Throws<KeyNotFoundException>(() => RequestData.GetSoapAction("NotThere"));

    [Fact]
    public void GetNamespace_ShouldReturnValidResult_WhenInputIsValid()
    {
        // Arrange
        string name = "GetAllLifeCycleDefinitions";
        string expectation = @"http://AutodeskDM/Services/LifeCycle/1/7/2020/";

        // Act
        string result = RequestData.GetNamespace(name);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetNamespace_ShouldThrowException_WhenInputIsInvalid()
        => Assert.Throws<KeyNotFoundException>(() => RequestData.GetNamespace("NotThere"));

    [Theory]
    [InlineData("SignIn", @"http://server/AutodeskDM/Services/Filestore/v26/AuthService.svc")]
    [InlineData("GetAllLifeCycleDefinitions", @"http://server/AutodeskDM/Services/v26/LifeCycleService.svc?op=GetAllLifeCycleDefinitions&currentCommand=Connectivity.Explorer.Admin.AdminToolsCommand")]
    public void GetRequestUri_ShouldReturnValidResult_WhenInputIsValid(string name, string expectation)
    {
        // Arrange
        string server = "server";

        // Act
        Uri result = RequestData.GetRequestUri(name, server);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void GetRequestUri_ShouldThrowException_WhenInputIsInvalid()
        => Assert.Throws<KeyNotFoundException>(() => RequestData.GetRequestUri("NotThere", "server"));
}
