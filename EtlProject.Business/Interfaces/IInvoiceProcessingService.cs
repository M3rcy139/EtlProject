namespace EtlProject.Business.Interfaces;

public interface IInvoiceProcessingService
{
    Task ProcessMessageAsync(string json);
}