using FluentVault.RequestBuilders;

namespace FluentVault;
public interface IUpdateRequestBuilder : IRequestBuilder
{
    public IUpdateFileRequestBuilder File { get; }
}
