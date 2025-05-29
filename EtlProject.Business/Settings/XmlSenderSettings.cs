namespace EtlProject.Business.Settings;

public class XmlSenderSettings
{
    public string HttpClientName { get; set; }
    public Dictionary<string, string> Endpoints { get; set; } = new();
}