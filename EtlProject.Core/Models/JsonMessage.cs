namespace EtlProject.Core.Models;

public class JsonMessage
{
    public Guid Id { get; set; }
    public string RawJson { get; set; } = string.Empty;
}