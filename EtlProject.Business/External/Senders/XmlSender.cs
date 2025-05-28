using System.Text;
using EtlProject.Business.Settings;
using EtlProject.Core.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EtlProject.Business.External.Senders;

public class XmlSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<XmlSender> _logger;
    private readonly XmlSenderSettings _settings;

    public XmlSender(IHttpClientFactory httpClientFactory, ILogger<XmlSender> logger, IOptions<XmlSenderSettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task SendAsync(string xml, string endpointKey)
    {
        if (!_settings.Endpoints.TryGetValue(endpointKey, out var url))
        {
            throw new ArgumentException(string.Format(ErrorMessages.EndpointNotFound, endpointKey));
        }
        
        var client = _httpClientFactory.CreateClient(_settings.HttpClientName);
        var content = new StringContent(xml, Encoding.UTF8, "text/xml");

        var response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"HTTP {response.StatusCode}: {error}");
        }

        _logger.LogInformation(InfoMessages.SentXmlSuccessfully);
    }
}