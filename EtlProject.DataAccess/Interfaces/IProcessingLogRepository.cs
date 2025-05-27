namespace EtlProject.DataAccess.Interfaces;

public interface IProcessingLogRepository
{
    Task<int> LogReceivedAsync(string json);
    Task LogSuccessAsync(int id);
    Task LogFailureAsync(int id, string error);
}