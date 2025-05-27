using EtlProject.Core.Enums;
using EtlProject.Core.Models;
using EtlProject.DataAccess.Interfaces;

namespace EtlProject.DataAccess.Repositories;

public class ProcessingLogRepository : IProcessingLogRepository
{
    private readonly EtlDbContext _context;

    public ProcessingLogRepository(EtlDbContext context)
    {
        _context = context;
    }

    public async Task<int> LogReceivedAsync(string json)
    {
        var log = new ProcessingLog
        {
            ReceivedAt = DateTime.UtcNow,
            RawJson = json,
            Status = ProcessingStatus.Received
        };

        _context.ProcessingLogs.Add(log);
        await _context.SaveChangesAsync();
        return log.Id;
    }

    public async Task LogSuccessAsync(int id)
    {
        var log = await _context.ProcessingLogs.FindAsync(id);
        if (log != null)
        {
            log.Status = ProcessingStatus.Sent;
            await _context.SaveChangesAsync();
        }
    }

    public async Task LogFailureAsync(int id, string error)
    {
        var log = await _context.ProcessingLogs.FindAsync(id);
        if (log != null)
        {
            log.Status = ProcessingStatus.Failed;
            log.ErrorMessage = error;
            await _context.SaveChangesAsync();
        }
    }
}