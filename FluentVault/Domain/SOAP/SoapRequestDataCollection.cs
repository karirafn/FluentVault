namespace FluentVault.Domain.SOAP;

internal class SoapRequestDataCollection
{
    public IEnumerable<SoapRequestData> SoapRequestData { get; set; } = new List<SoapRequestData>();
}
