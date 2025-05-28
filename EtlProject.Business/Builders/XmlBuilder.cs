using System.Xml.Linq;
using EtlProject.Core.Dtos;

namespace EtlProject.Business.Builders;

public class XmlBuilder
{
    public string BuildXml(InvoiceRequest invoice)
    {
        var doc = new XDocument(
            new XElement("invoice_payment",
                new XElement("id", invoice.Id),
                new XElement("debit", invoice.DebitAccount),
                new XElement("credit", invoice.CreditAccount),
                new XElement("amount", invoice.Amount),
                new XElement("currency", invoice.Currency),
                new XElement("details", invoice.Details),
                new XElement("pack", invoice.Pack ?? string.Empty)
            )
        );

        return doc.ToString(SaveOptions.DisableFormatting);
    }
}