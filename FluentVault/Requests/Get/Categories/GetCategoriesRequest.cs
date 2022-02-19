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
        string innerBody = GetOpeningTag(isSelfClosing: true);
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        IEnumerable<VaultCategory> categories = document.ParseCategories();

        return categories;
    }
}
