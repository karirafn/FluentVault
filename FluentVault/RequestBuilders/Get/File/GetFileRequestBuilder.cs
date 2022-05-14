
using FluentVault.RequestBuilders.Get.File.Associations;

namespace FluentVault.RequestBuilders.Get.File;
internal class GetFileRequestBuilder : IRequestBuilder, IGetFileRequestBuilder
{
    public GetFileRequestBuilder(IGetFileAssociationsRequestBuilder associations, IGetFilePropertiesRequestBuilder properties)
    {
        Associations = associations;
        Properties = properties;
    }

    public IGetFileAssociationsRequestBuilder Associations { get; }
    public IGetFilePropertiesRequestBuilder Properties { get; }
}
