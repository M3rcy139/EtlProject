using EtlProject.Core.Enums;

namespace EtlProject.Core.Models;

public class ProcessingLog
{
    public int Id { get; set; }
    public DateTime ReceivedAt { get; set; }
    public string RawJson { get; set; } = string.Empty;
    public ProcessingStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}