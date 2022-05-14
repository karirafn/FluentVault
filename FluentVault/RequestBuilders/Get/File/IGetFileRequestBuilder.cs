
using FluentVault.RequestBuilders.Get.File.Associations;

namespace FluentVault;

public interface IGetFileRequestBuilder
{
    IGetFileAssociationsRequestBuilder Associations { get; }
    IGetFilePropertiesRequestBuilder Properties { get; }
}
