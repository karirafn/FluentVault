using System.Text;

namespace FluentVault;

internal static class BodyBuilder
{
    internal static string GetRequestBody(string innerBody, Guid? ticket = null, long? userId = null)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine(@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">");

        if (ticket is not null && userId is not null)
        {
            bodyBuilder.Append(GetHeaderBody(ticket, userId));
        }

        bodyBuilder.AppendLine(@"   <s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">");
        bodyBuilder.Append(innerBody);
        bodyBuilder.AppendLine("    </s:Body>");
        bodyBuilder.AppendLine("</s:Envelope>");

        return bodyBuilder.ToString();
    }

    private static string GetHeaderBody(Guid? ticket, long? userId)
    {
        StringBuilder bodyBuilder = new();
        bodyBuilder.AppendLine("    <s:Header>");
        bodyBuilder.AppendLine(@"       <SecurityHeader xmlns=""http://AutodeskDM/Services"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">");
        bodyBuilder.AppendLine($"           <Ticket>{ticket}</Ticket>");
        bodyBuilder.AppendLine($"           <UserId>{userId}</UserId>");
        bodyBuilder.AppendLine("        </SecurityHeader>");
        bodyBuilder.AppendLine("    </s:Header>");

        return bodyBuilder.ToString();
    }
}
