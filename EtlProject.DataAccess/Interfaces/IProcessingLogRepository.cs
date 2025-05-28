using EtlProject.Core.Enums;
using EtlProject.Core.Models;

namespace EtlProject.DataAccess.Interfaces;

public interface IProcessingLogRepository
{
    Task<Guid> AddProcessingLogAsync(ProcessingLog processingLog);
    Task UpdateLogStatusAsync(ProcessingLog processingLog, ProcessingStatus status, string? error = null);
}