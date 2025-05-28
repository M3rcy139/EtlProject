namespace EtlProject.Core.Dtos;

public record InvoiceRequest(
    long Id,
    string DebitAccount,
    string CreditAccount,
    decimal Amount,
    string Currency,
    string Details,
    string? Pack);