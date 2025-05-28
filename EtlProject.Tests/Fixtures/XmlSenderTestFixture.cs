using System.Net;
using EtlProject.Business.External.Senders;
using EtlProject.Business.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace EtlProject.Tests.Fixtures;

public class XmlSenderTestFixture
{
    public Mock<HttpMessageHandler> HandlerMock { get; }
    public Mock<IHttpClientFactory> FactoryMock { get; }
    public Mock<ILogger<XmlSender>> LoggerMock { get; }
    public XmlSender Sender { get; }
    
    public readonly string TestEndpointKey = "Invoice";
    private const string TestEndpointUrl = "https://test-endpoint/api";
    private const string HttpClientName = "XmlSenderClient";

    public XmlSenderTestFixture()
    {
        HandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(HandlerMock.Object);
        
        FactoryMock = new Mock<IHttpClientFactory>();
        FactoryMock.Setup(f => f.CreateClient(HttpClientName)).Returns(httpClient);

        LoggerMock = new Mock<ILogger<XmlSender>>();

        var options = Options.Create(new XmlSenderSettings
        {
            HttpClientName = HttpClientName,
            Endpoints = new Dictionary<string, string>
            {
                { TestEndpointKey, TestEndpointUrl }
            }
        });

        Sender = new XmlSender(FactoryMock.Object, LoggerMock.Object, options);
    }

    public void SetupHttpResponse(HttpStatusCode statusCode, string content)
    {
        HandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            });
    }

    public void VerifyLogInformationContains(string expectedMessage, Times times)
    {
        LoggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(expectedMessage)),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}