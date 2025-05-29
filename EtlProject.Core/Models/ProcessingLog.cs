using EtlProject.Core.Enums;

namespace EtlProject.Core.Models;

public class ProcessingLog
{
    public Guid Id { get; set; }
    public DateTime ReceivedAt { get; set; }
    public ProcessingStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public Guid JsonId { get; set;  }
    
    public JsonMessage JsonMessage { get; set; }
}