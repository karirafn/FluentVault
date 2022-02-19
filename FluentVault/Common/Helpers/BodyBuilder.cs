using System.Text;

namespace FluentVault.Common.Helpers;

internal static class BodyBuilder
{
    internal static string GetRequestBody(string innerBody, Guid ticket = new(), long userId = -1)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">");

        if (ticket != Guid.Empty && userId >= 0)
        {
            bodyBuilder.Append(GetHeaderBody(ticket, userId));
        }

        bodyBuilder.AppendLine(@"<s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">");
        bodyBuilder.AppendLine(innerBody);
        bodyBuilder.AppendLine("</s:Body>");
        bodyBuilder.Append("</s:Envelope>");

        return bodyBuilder.ToString();
    }

    private static string GetHeaderBody(Guid ticket, long userId)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine("<s:Header>");
        bodyBuilder.AppendLine(@"<SecurityHeader xmlns=""http://AutodeskDM/Services"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">");
        bodyBuilder.AppendLine($"<Ticket>{ticket}</Ticket>");
        bodyBuilder.AppendLine($"<UserId>{userId}</UserId>");
        bodyBuilder.AppendLine("</SecurityHeader>");
        bodyBuilder.AppendLine("</s:Header>");

        return bodyBuilder.ToString();
    }
}
