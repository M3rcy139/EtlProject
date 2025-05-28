using System.Text.Json.Nodes;
using EtlProject.Core.Dtos;

namespace EtlProject.Business.Mappers;

public class InvoiceJsonMapper
{
    public InvoiceRequest Map(string json)
    {
        var node = JsonNode.Parse(json)!;

        var id = node["request"]?["id"]?.GetValue<long>() ?? throw new("Missing request.id");
        var debit = node["debitPart"]!;
        var credit = node["creditPart"]!;

        var debitAccount = debit["accountNumber"]?.GetValue<string>()!;
        var creditAccount = credit["accountNumber"]?.GetValue<string>()!;
        var amount = debit["amount"]?.GetValue<decimal>() ?? 0;
        var currency = debit["currency"]?.GetValue<string>() ?? "";
        var details = node["details"]?.GetValue<string>() ?? "";

        var pack = node["attributes"]?["attribute"]?
            .AsArray()
            .FirstOrDefault(x => x?["code"]?.GetValue<string>() == "pack")?
            ["attribute"]?.GetValue<string>();

        return new InvoiceRequest(id, debitAccount, creditAccount, amount, currency, details, pack);
    }
}