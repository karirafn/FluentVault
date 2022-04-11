using System.Xml.Linq;

using FluentVault.Domain.Search;
using FluentVault.Extensions;
using FluentVault.TestFixtures.File;

namespace FluentVault.TestFixtures.Search;
internal class VaultFileSearchResultFixture : VaultEntityRequestFixture<VaultFileSearchResult>
{
    private static readonly VaultFileFixture _fixture = new();

    public VaultFileSearchResultFixture() : base("FindFilesBySearchConditions", "http://AutodeskDM/Services/Document/1/7/2020/")
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultFileSearchResult entity)
    {
        // Use ParseXDocument instead as response has content
        throw new NotImplementedException();
    }

    public override XDocument ParseXDocument(VaultFileSearchResult search)
    {
        XElement bookmark = new(Namespace + "bookmark", search.Bookmark);
        XElement searchStatus = new(Namespace + "searchstatus");
        searchStatus.AddAttribute("TotalHits", search.SearchStatus.TotalHits);
        searchStatus.AddAttribute("IndxStatus", search.SearchStatus.IndexingStatus);
        IEnumerable<XElement> responseContent = new[] { bookmark, searchStatus };
        IEnumerable<XElement> resultContent = search.Files.Select(file => _fixture.ParseXElement(file));

        return new XDocument().AddResponseContent(Operation, Namespace, responseContent, resultContent);
    }
}
