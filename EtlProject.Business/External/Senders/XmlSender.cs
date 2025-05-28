using System.Text;
using EtlProject.Core.Constants;
using Microsoft.Extensions.Logging;

namespace EtlProject.Business.External.Senders;

public class XmlSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<XmlSender> _logger;

    public XmlSender(IHttpClientFactory httpClientFactory, ILogger<XmlSender> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task SendAsync(string xml)
    {
        var client = _httpClientFactory.CreateClient();
        var content = new StringContent(xml, Encoding.UTF8, "text/xml");

        var response = await client.PostAsync("https://localhost:7057/api/v1/invoice", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"HTTP {response.StatusCode}: {error}");
        }

        _logger.LogInformation(InfoMessages.SentXmlSuccessfully);
    }
}