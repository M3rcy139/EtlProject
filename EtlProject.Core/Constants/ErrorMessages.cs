namespace EtlProject.Core.Constants;

public static class ErrorMessages
{
    public const string ErrorProcessing = "Error processing message";
    public const string MissingRequestId = "Missing request.id";
    public const string MissingDebitAccount = "Missing debit account";
    public const string MissingCreditAccount = "Missing credit account";
    public const string ErrorMessageFromRabbitMq = "Error processing message from RabbitMQ";
    public const string EndpointNotFound = "No endpoint configured for key: {0}.";
}