using System.Text.Json.Nodes;
using EtlProject.Business.Extensions;
using EtlProject.Core.Constants;
using EtlProject.Core.Dtos;

namespace EtlProject.Business.Mappers;

public class InvoiceJsonMapper
{
    public InvoiceRequest Map(string json)
    {
        var node = JsonNode.Parse(json)!;

        var id = node["request"]?["id"]?.GetValue<long>() 
                 ?? throw new ArgumentException(ErrorMessages.MissingRequestId);
        var debit = node["debitPart"]!;
        var credit = node["creditPart"]!;

        var debitAccount = debit["accountNumber"]?.GetValue<string>()
                           ?? throw new ArgumentException(ErrorMessages.MissingDebitAccount);
        var creditAccount = credit["accountNumber"]?.GetValue<string>()
                            ?? throw new ArgumentException(ErrorMessages.MissingCreditAccount);

        var debitAgreement = debit["agreementNumber"]?.GetValue<string>()
                             ?? throw new ArgumentException(ErrorMessages.MissingDebitAgreementNumber);
        var creditAgreement = credit["agreementNumber"]?.GetValue<string>()
                              ?? throw new ArgumentException(ErrorMessages.MissingCreditAgreementNumber);

        var amount = debit["amount"]?.GetValue<decimal>() ?? 0;
        var currency = debit["currency"]?.GetValue<string>() ?? "";
        var details = node["details"]?.GetValue<string>() ?? "";

        var attributes = node.ToAttributeDictionary();

        return new InvoiceRequest(id, debitAccount, creditAccount, amount, currency, details, attributes, 
            debitAgreement, creditAgreement);
    }
}