
using Ardalis.SmartEnum;

namespace FluentVault.Domain.Search;
public sealed class IndexingStatus : SmartEnum<IndexingStatus>
{
    public static readonly IndexingStatus IndexingComplete = new(nameof(IndexingComplete), 1);
    public static readonly IndexingStatus IndexingContent = new(nameof(IndexingContent), 2);
    public static readonly IndexingStatus IndexingProperties = new(nameof(IndexingProperties), 3);

    private IndexingStatus(string name, int value) : base(name, value) { }
}
