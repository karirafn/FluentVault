using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Search;

public class SearchFilesTests : BaseRequestTest
{
    [Fact]
    public async Task SearchFilesWithoutPaging_ShouldReturnMoreResultsThanThePagingLimit_WhenInputsAreValid()
    {
        // This test will fail if the number of assemblies modified in the last month
        // that are still in the "In Work" state is less than the paging limit (default is 200)

        // Arrange
        string searchValue = "in work iam";
        DateTime datetime = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueContaining(searchValue)
            .InAllProperties
            .And
            .ForValueGreaterThan(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithoutPaging();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCountGreaterThan(200);
    }
}
