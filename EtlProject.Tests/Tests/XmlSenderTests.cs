using System.Net;
using EtlProject.Business.External.Senders;
using EtlProject.Core.Constants;
using EtlProject.Tests.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace EtlProject.Tests.Tests;

public class XmlSenderTests
{
    private readonly XmlSenderTestFixture _fixture;

    public XmlSenderTests()
    {
        _fixture = new XmlSenderTestFixture();
    }
    
    [Fact]
    public async Task SendAsync_Should_Send_Http_Request_And_Log_Success()
    {
        // Arrange
        _fixture.SetupHttpResponse(HttpStatusCode.OK, "<result>ok</result>");

        // Act
        await _fixture.Sender.SendAsync("<xml></xml>");

        // Assert
        _fixture.VerifyLogInformationContains(InfoMessages.SentXmlSuccessfully, Times.Once());
    }

    [Fact]
    public async Task SendAsync_Should_Throw_On_NonSuccessStatusCode()
    {
        // Arrange
        _fixture.SetupHttpResponse(HttpStatusCode.BadRequest, "Bad Request");

        // Act & Assert
        var ex = await Assert.ThrowsAsync<HttpRequestException>(() => _fixture.Sender.SendAsync("<xml></xml>"));
        Assert.Contains("HTTP BadRequest", ex.Message);
    }
}