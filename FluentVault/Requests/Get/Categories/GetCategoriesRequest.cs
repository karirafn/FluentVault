using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Helpers;
using FluentVault.Domain.Category;

namespace FluentVault.Requests.Get.Categories;

internal class GetCategoriesRequest : SessionRequest
{
    public GetCategoriesRequest(VaultSession session)
        : base(session, RequestData.GetCategoryConfigurationsByBehaviorNames) { }

    public async Task<IEnumerable<VaultCategory>> SendAsync()
    {
        StringBuilder innerBody = GenerateInnerBodyFromRequestData();
        XDocument document = await SendRequestAsync(innerBody);
        IEnumerable<VaultCategory> categories = document.ParseCategories();

        return categories;
    }
}
