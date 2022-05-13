
using FluentVault.RequestBuilders.Get.File.Associations;

namespace FluentVault.RequestBuilders.Get.File;
internal class GetFileRequestBuilder : IRequestBuilder, IGetFileRequestBuilder
{
    public GetFileRequestBuilder(IGetFileAssociationsRequestBuilder associations)
    {
        Associations = associations;
    }

    public IGetFileAssociationsRequestBuilder Associations { get; }
}
