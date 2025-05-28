using EtlProject.Business.Builders;
using EtlProject.Business.External.Senders;
using EtlProject.Business.Interfaces;
using EtlProject.Business.Mappers;
using EtlProject.Core.Constants;
using EtlProject.Core.Enums;
using EtlProject.Core.Models;
using EtlProject.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace EtlProject.Business.Services;
public class InvoiceProcessingService : IInvoiceProcessingService
{
    private readonly IProcessingLogRepository _logRepo;
    private readonly IJsonMessageRepository _jsonRepo;
    private readonly InvoiceJsonMapper _mapper;
    private readonly XmlBuilder _xmlBuilder;
    private readonly XmlSender _sender;
    private readonly ILogger<InvoiceProcessingService> _logger;

    public InvoiceProcessingService(
        IProcessingLogRepository logRepo,
        IJsonMessageRepository jsonRepo,
        InvoiceJsonMapper mapper,
        XmlBuilder xmlBuilder,
        XmlSender sender,
        ILogger<InvoiceProcessingService> logger)
    {
        _logRepo = logRepo;
        _jsonRepo = jsonRepo;
        _mapper = mapper;
        _xmlBuilder = xmlBuilder;
        _sender = sender;
        _logger = logger;
    }

    public async Task ProcessMessageAsync(string json)
    {
        var jsonId = await AddJsonMessageAsync(json);
        var processingLog = await AddProcessingLogAsync(jsonId);

        try
        {
            var xml = BuildXml(json);
            await _sender.SendAsync(xml);
            
            await _logRepo.UpdateLogStatusAsync(processingLog, ProcessingStatus.Sent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorMessages.ErrorProcessing);
            await _logRepo.UpdateLogStatusAsync(processingLog, ProcessingStatus.Failed, ex.Message);
        }
    }

    private async Task<Guid> AddJsonMessageAsync(string json)
    {
        var jsonMessage = new JsonMessage()
        {
            Id = Guid.NewGuid(),
            RawJson = json,
        };
        
        await _jsonRepo.AddJsonMessage(jsonMessage);

        return jsonMessage.Id;
    }

    private async Task<ProcessingLog> AddProcessingLogAsync(Guid jsonId)
    {
        var processingLog = new ProcessingLog
        {
            Id = Guid.NewGuid(),
            JsonId = jsonId,
            ReceivedAt = DateTime.UtcNow,
            Status = ProcessingStatus.Received
        };

        await _logRepo.AddProcessingLogAsync(processingLog);
        
        return processingLog;
    }
    
    private string BuildXml(string json)
    {
        var invoice = _mapper.Map(json);
        return _xmlBuilder.BuildXml(invoice);
    }
}