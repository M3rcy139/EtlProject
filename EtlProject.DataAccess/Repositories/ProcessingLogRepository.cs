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

    public async Task<Guid> AddProcessingLogAsync(ProcessingLog processingLog)
    {
        await _context.ProcessingLogs.AddAsync(processingLog);
        await _context.SaveChangesAsync();
        
        return processingLog.Id;
    }
    
    public async Task UpdateLogStatusAsync(ProcessingLog processingLog, ProcessingStatus status, string? error = null)
    {
        processingLog.Status = status;
        processingLog.ErrorMessage = error;
        await _context.SaveChangesAsync();
    }
}