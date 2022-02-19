using System.Text;
using System.Xml.Linq;

namespace FluentVault;

internal class UpdateFileLifecycleStateRequest : SessionRequest, IUpdateFileLifecycleStateRequest, IWithFileMasterId, IWithComment
{
    private long _masterId;
    private long _stateId;

    public UpdateFileLifecycleStateRequest(VaultSession session)
        : base(session, RequestData.UpdateFileLifeCycleStates) { }

    public IWithFileMasterId WithMasterId(long masterId)
    {
        _masterId = masterId;
        return this;
    }

    public IWithComment ToStateWithId(long stateId)
    {
        _stateId = stateId;
        return this;
    }

    public async Task<VaultFile> WithComment(string comment)
    {
        string innerBody = GetInnerBody(_masterId, _stateId, comment);
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        VaultFile file = document.ParseVaultFile();

        return file;
    }

    public async Task<VaultFile> WithoutComment() => await WithComment(string.Empty);

    private string GetInnerBody(long masterId, long stateId, string comment)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(GetOpeningTag());
        bodyBuilder.AppendLine("<fileMasterIds>");
        bodyBuilder.AppendLine($"<long>{masterId}</long>");
        bodyBuilder.AppendLine("</fileMasterIds>");
        bodyBuilder.AppendLine("<toStateIds>");
        bodyBuilder.AppendLine($"<long>{stateId}</long>");
        bodyBuilder.AppendLine("</toStateIds>");
        bodyBuilder.AppendLine($"<comment>{comment}</comment>");
        bodyBuilder.AppendLine(GetClosingTag());

        return bodyBuilder.ToString();
    }
}
